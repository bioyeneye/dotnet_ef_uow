using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using CoreLibrary.AutoFac;
using CoreLibrary.BusinessLogic.Service;
using CoreLibrary.Data.EF;
using CoreLibrary.Data.Repositories;
using CoreLibrary.Test.AppStart;
using CoreLibrary.Test.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace CoreLibrary.Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }
        public static IConfiguration Configuration { get; set; }
        public IContainer ApplicationContainer { get; private set; }
        private IHostingEnvironment HostingEnvironment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
                });

            var connection = Configuration["Data:SqlServerConnectionString"];
            services.AddIdentityDbContext<ApplicationDbContext>(connection);
            services.AddDbContext<ProjectmeetingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(connection)));

            services.RegisterLibraryServices<ProjectmeetingContext, MeetingRepository, MeetingService>();
            /*services.RegisterApplicationRepository<MeetingRepository>();
            services.RegisterApplicationServices<MeetingService>();*/
            services.AddIdentity();
            services.AddOpenIddict(HostingEnvironment);
            services.RegisterBasicDiFromLibrary();
            services.RegisterCustomServices(); 

            var builder = new ContainerBuilder();
            builder.Populate(services);
            
            ApplicationContainer = builder.Build();
            ApplicationContainer.SetLifetimeScope();
             
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            return new AutofacServiceProvider(ApplicationContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env
            ,ILoggerFactory loggerFactory
            , IServiceProvider svp)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}