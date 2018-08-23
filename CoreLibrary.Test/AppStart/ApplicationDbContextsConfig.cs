using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Test.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLibrary.Test.AppStart
{
    public static class ApplicationDbContextsConfig
    {
        public static IServiceCollection AddIdentityDbContext<T>(this IServiceCollection services, string sqlConnection) where T : DbContext
        {
            services.AddDbContextPool<T>(options =>
            {
                string useSqLite = Startup.Configuration["Data:useSqLite"];
                string useInMemory = Startup.Configuration["Data:useInMemory"];
                if (useInMemory.ToLower() == "true")
                {
                    options.UseInMemoryDatabase("ApplicationDbContext"); // Takes database name
                }
                else if (useSqLite.ToLower() == "true")
                {
                    //install sqlLite
                    /*var connection = Startup.Configuration["Data:SqlLiteConnectionString"];
                    options.UseSqlite(connection);
                    options.UseSqlite(connection, b => b.MigrationsAssembly("AspNetCoreSpa.Web"));*/

                }
                else
                {
                    options.UseSqlServer(sqlConnection);
                    options.UseSqlServer(sqlConnection, b => b.MigrationsAssembly("CoreLibrary.Test"));
                }
                options.UseOpenIddict();
            });
            return services;
        }
    }
}
