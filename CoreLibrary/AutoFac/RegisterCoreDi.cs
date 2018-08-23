using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CoreLibrary.AutoFac
{
    public static class RegisterCoreDi
    {
        public static void RegisterBasicDiFromLibrary(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }
    }
}
