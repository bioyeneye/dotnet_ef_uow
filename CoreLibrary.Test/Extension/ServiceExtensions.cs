using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.DataContext;
using CoreLibrary.EntityFramework;
using CoreLibrary.Repositories;
using CoreLibrary.Test.EF;
using CoreLibrary.Test.Models;
using CoreLibrary.Test.Repository;
using CoreLibrary.Test.Service;
using CoreLibrary.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLibrary.Test.Extension
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterLibraryServices(this IServiceCollection services)
        {
            services.AddTransient<IDataContextAsync, ProjectmeetingContext>();
            services.AddTransient<IUnitOfWorkAsync, EntityFrameorkUnitOfWork>();
            services.AddTransient<IUnitOfWork, EntityFrameorkUnitOfWork>();
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IMeetingRepository, MeetingRepository>();
            services.AddTransient<IMeetingService, MeetingService>(); 

            // Add all other services here.
            return services;
        }


        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IMeetingRepository, MeetingRepository>();
            services.AddTransient<IMeetingService, MeetingService>();
        }
    }
}
