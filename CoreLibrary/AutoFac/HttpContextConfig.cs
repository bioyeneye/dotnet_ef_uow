using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.AspNetCore.Http;

namespace CoreLibrary.AutoFac
{
    public class HttpContextConfig
    {
        private static IServiceProvider ServiceProvider;
        public static HttpContext CurrentHttpContext
        {
            get
            {
                var factory = ServiceProvider.GetService(typeof(IHttpContextAccessor)) ?? new HttpContextAccessor();
                HttpContext context = ((HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
    }
}
