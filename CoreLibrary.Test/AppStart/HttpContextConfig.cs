using System;
using Microsoft.AspNetCore.Http;

namespace CoreLibrary.Test.AppStart
{
    public class HttpContextConfig
    {
        public static IServiceProvider ServiceProvider;
        public static HttpContext Current
        {
            get
            {
                // var factory2 = ServiceProvider.GetService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
                object factory = ServiceProvider.GetService(typeof(IHttpContextAccessor))??new HttpContextAccessor();

                // Microsoft.AspNetCore.Http.HttpContextAccessor fac =(Microsoft.AspNetCore.Http.HttpContextAccessor)factory;
                HttpContext context = ((HttpContextAccessor)factory).HttpContext;
                // context.Response.WriteAsync("Test");

                return context;
            }
        }
    }
}
