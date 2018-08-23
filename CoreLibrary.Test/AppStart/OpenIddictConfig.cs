using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using CoreLibrary.Test.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLibrary.Test.AppStart
{
    public static class OpenIddictConfig
    {
        public static IServiceCollection AddOpenIddict(this IServiceCollection services, IHostingEnvironment env)
        {
            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // Register the OpenIddict services.
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<ApplicationDbContext>();
                })
                .AddServer(options =>
                {

                    // Register the ASP.NET Core MVC binder used by OpenIddict.
                    // Note: if you don't call this method, you won't be able to
                    // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                    options.UseMvc();

                    // Enable the token endpoint.
                    // Form password flow (used in username/password login requests)
                    options.EnableTokenEndpoint("/connect/token");

                    // Enable the authorization endpoint.
                    // Form implicit flow (used in social login redirects)
                    options.EnableAuthorizationEndpoint("/connect/authorize");

                    // Enable the password and the refresh token flows.
                    options.AllowPasswordFlow()
                           .AllowRefreshTokenFlow()
                           .AllowImplicitFlow(); // To enable external logins to authenticate

                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30));
                    options.SetIdentityTokenLifetime(TimeSpan.FromMinutes(30));
                    options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(60));
                    // During development, you can disable the HTTPS requirement.
                    if (env.IsDevelopment())
                    {
                        options.DisableHttpsRequirement();
                    }

                    // Note: to use JWT access tokens instead of the default
                    // encrypted format, the following lines are required:
                    //
                    // options.UseJsonWebTokens();
                    options.AddEphemeralSigningKey();
                });

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

            services.AddAuthentication(options =>
            {
                // This will override default cookies authentication scheme
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddOAuthValidation()
               // https://console.developers.google.com/projectselector/apis/library?pli=1
               .AddGoogle(options =>
               {
                   options.ClientId = Startup.Configuration["Authentication:Google:ClientId"];
                   options.ClientSecret = Startup.Configuration["Authentication:Google:ClientSecret"];
               })
               // https://developers.facebook.com/apps
               .AddFacebook(options =>
               {
                   options.AppId = Startup.Configuration["Authentication:Facebook:AppId"];
                   options.AppSecret = Startup.Configuration["Authentication:Facebook:AppSecret"];
               })
               // https://apps.twitter.com/
               .AddTwitter(options =>
               {
                   options.ConsumerKey = Startup.Configuration["Authentication:Twitter:ConsumerKey"];
                   options.ConsumerSecret = Startup.Configuration["Authentication:Twitter:ConsumerSecret"];
               })
               // https://apps.dev.microsoft.com/?mkt=en-us#/appList
               .AddMicrosoftAccount(options =>
               {
                   options.ClientId = Startup.Configuration["Authentication:Microsoft:ClientId"];
                   options.ClientSecret = Startup.Configuration["Authentication:Microsoft:ClientSecret"];
               });

            return services;
        }
    }
}
