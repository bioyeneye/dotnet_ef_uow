using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.BusinessLogic.Service;
using CoreLibrary.Data.EF;
using CoreLibrary.Data.Repositories;
using CoreLibrary.DataContext;
using CoreLibrary.EntityFramework;
using CoreLibrary.Repositories; 
using CoreLibrary.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLibrary.Test.Extension
{
    public static class ServiceExtensions
    {
        


        public static void RegisterServices(IServiceCollection services)
        {
            
            services.AddTransient<IMeetingRepository, MeetingRepository>();
            services.AddTransient<IMeetingService, MeetingService>();
        }
    }
}
