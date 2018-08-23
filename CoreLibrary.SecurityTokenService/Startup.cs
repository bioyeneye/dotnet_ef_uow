using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using CoreLibrary.SecurityTokenService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLibrary.SecurityTokenService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        private IHostingEnvironment HostingEnvironment { get; }
        public static IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors();
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseOpenIddict();
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services.AddOpenIddict()

                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                           .UseDbContext<ApplicationDbContext>();
                })

                .AddServer(options =>
                {
                    options.UseMvc();
                    options.EnableTokenEndpoint("/connect/token");
                    options.AllowPasswordFlow()
                           .AllowRefreshTokenFlow();

                    options.AcceptAnonymousClients();

                    options.DisableHttpsRequirement();

                    // Note: to use JWT access tokens instead of the default
                    // encrypted format, the following lines are required:
                    //
                    // options.UseJsonWebTokens();
                    // options.AddEphemeralSigningKey();
                })

                .AddValidation();

            // If you prefer using JWT, don't forget to disable the automatic
            // JWT -> WS-Federation claims mapping used by the JWT middleware:
            //
            // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            // JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            //
            // services.AddAuthentication()
            //     .AddJwtBearer(options =>
            //     {
            //         options.Authority = "http://localhost:54895/";
            //         options.Audience = "resource_server";
            //         options.RequireHttpsMetadata = false;
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             NameClaimType = OpenIdConnectConstants.Claims.Subject,
            //             RoleClaimType = OpenIdConnectConstants.Claims.Role
            //         };
            //     });

            // Alternatively, you can also use the introspection middleware.
            // Using it is recommended if your resource server is in a
            // different application/separated from the authorization server.
            //
            // services.AddAuthentication()
            //     .AddOAuthIntrospection(options =>
            //     {
            //         options.Authority = new Uri("http://localhost:54895/");
            //         options.Audiences.Add("resource_server");
            //         options.ClientId = "resource_server";
            //         options.ClientSecret = "875sqd4s5d748z78z7ds1ff8zz8814ff88ed8ea4z4zzd";
            //         options.RequireHttpsMetadata = false;
            //     });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:5055");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
