/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DataAccess.EFCore;
using Common.DataAccess.EFCore.Repositories;
using Common.Services;
using Common.Services.Infrastructure;
using Common.WebApiCore.Identity;
using Common.WebApiCore.Setup;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using AutoMapperConfiguration = AutoMapper.Configuration;

namespace Common.WebApiCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        protected void ConfigureDependencies(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("localDb");
            DependenciesConfig.ConfigureDependencies(services, connectionString);
        }

        protected void ConfigureIdentity(IServiceCollection services)
        {
            IdentityConfig.Configure(services);
        }

        protected void ConfigureMapping(AutoMapperConfiguration.MapperConfigurationExpression config)
        {
            AutoMapperConfig.Configure(config);
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            ConfigureIdentity(services);
            services.ConfigureAuth(Configuration);
            ConfigureDependencies(services);
            RegisterMapping();

            services.ConfigureSwagger();

            services.ConfigureCors();

            services.AddAuthorization(opt => opt.RegisterPolicies());

            services
                .AddControllers(opt =>
                {
                    opt.UseCentralRoutePrefix(new RouteAttribute("api"));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                           .UseSimpleAssemblyNameTypeSerializer()
                                           .UseDefaultTypeSerializer()
                                           .UseMemoryStorage());
            services.AddHangfireServer();
            
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env, IDataBaseInitializer dataBaseInitializer)
        {
            if (dataBaseInitializer != null)
            {
                dataBaseInitializer.Initialize();
            }
            else
            {
                // TODO: add logging
            }

            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseConfiguredSwagger();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate(() => new BackgroundJobService(new HotspotResultService(new HotspotResultRepository(new DataContext()))).Recurring(), "*/3 * * * *");
        }

        private void RegisterMapping()
        {
            var config = new AutoMapperConfiguration.MapperConfigurationExpression();
            AutoMapperConfig.Configure(config);
            ConfigureMapping(config);
            AutoMapper.Mapper.Initialize(config);
        }
    }
}
