using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Test.Filter;
using CoreLibrary.Test.Models;
using CoreLibrary.UnitOfWork;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace CoreLibrary.Test.AppStart
{
    public static class CustomServicesConfig
    {
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            // New instance every time, only configuration class needs so its ok
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<UserResolverService>();
            services.AddScoped<ApiExceptionFilter>();
            return services;
        }
    }
}
