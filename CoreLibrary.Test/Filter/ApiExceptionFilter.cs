using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Test.Filter
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;
        private readonly IHostingEnvironment _env;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }


        public override void OnException(ExceptionContext context)
        {
            ApiError apiError = null;
            if (context.Exception is ApiException ex)
            {
                // handle explicit 'known' API errors
                context.Exception = null;
                apiError = new ApiError(ex.Message) {Errors = ex.Errors};

                context.HttpContext.Response.StatusCode = ex.StatusCode;

                _logger.LogError($"Application thrown error: {ex.Message}", ex);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;
                _logger.LogError("Unauthorized Access in Controller Filter.");
            }
            else
            {
                // Unhandled errors
                var msg = "";
                var stack = "";
                if (_env.IsDevelopment())
                {
                    msg = context.Exception.GetBaseException().Message;
                    stack = context.Exception.StackTrace;
                }
                else
                {
                    msg = "An unhandled error occurred.";
                    stack = null;
                }

                apiError = new ApiError(msg) {detail = stack};

                context.HttpContext.Response.StatusCode = 500;

                // handle logging here
                _logger.LogError(new EventId(0), context.Exception, msg);
            }

            // always return a JSON result
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }
    }
}
