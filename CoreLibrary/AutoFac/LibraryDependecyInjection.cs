using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CoreLibrary.DataContext;
using CoreLibrary.EntityFramework;
using CoreLibrary.Repositories;
using CoreLibrary.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace CoreLibrary.AutoFac
{
    public static class LibraryDependecyInjection
    {
        /// <summary>
        /// Register the Repository.Pattern to your application
        /// </summary>
        /// <typeparam name="TApplicationContext">Application DbContext inheriting from from EntityFrameworkDataContext</typeparam>
        /// <typeparam name="TRepository"></typeparam>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterLibraryServices<TApplicationContext, TRepository,TService>(this IServiceCollection services) 
            where TApplicationContext : EntityFrameworkDataContext<TApplicationContext> where TRepository : class where TService : class 
        {
            services.AddTransient<IDataContextAsync, TApplicationContext>();
            services.AddTransient<IUnitOfWorkAsync, EntityFrameorkUnitOfWork>();
            services.AddTransient<IUnitOfWork, EntityFrameorkUnitOfWork>();
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            //Repository
            var assemblyReposityToScan = Assembly.GetAssembly(typeof(TRepository));
            services.RegisterAssemblyPublicNonGenericClasses(assemblyReposityToScan)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces();

            //Service
            var assemblyserviceToScan = Assembly.GetAssembly(typeof(TService));
            services.RegisterAssemblyPublicNonGenericClasses(assemblyserviceToScan)
                .Where(x => x.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();



            return services;
        }

        /// <summary>
        /// Register Services to your application
        /// </summary>
        /// <typeparam name="T">Sample application service class</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterApplicationServices<T>(this IServiceCollection services)
        {
            var assemblyserviseToScan = Assembly.GetAssembly(typeof(T));
            services.RegisterAssemblyPublicNonGenericClasses(assemblyserviseToScan)
                .Where(x => x.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            return services;
        }

        /// <summary>
        /// Register Repositories to your application
        /// </summary>
        /// <typeparam name="T">Sample application repository class</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterApplicationRepository<T>(this IServiceCollection services)
        {
            var assemblyserviseToScan = Assembly.GetAssembly(typeof(T));
            services.RegisterAssemblyPublicNonGenericClasses(assemblyserviseToScan)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces();

            return services;
        }
    }
}
