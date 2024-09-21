using Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Reflection;
using Data.Contracts;
using Data.Repositories;
using Microsoft.Extensions.Hosting;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.Middlewares;
using WebFramework.Swagger;

namespace FoodStuff.API
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddCustomCors();

            services.InitializeAutoMapper();

            services.AddDbContext(Configuration);

            services.AddMinimalMvc();

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddMediatR(typeof(Startup));

            services.AddLocalizations();

            services.AddSwagger();

            services.AddRedis(Configuration);

            services.AddCustomRedis(_siteSetting.RedisClient);

            services.AddCustomDapper(Configuration);

            return services.BuildAutofacServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();
            //app.UseDeveloperExceptionPage();
            app.UseHsts(env);

            //if (env.IsDevelopment())
            //{
            app.UseSwaggerAndUI();
            //}

            app.UseRouting();

            app.UseRequestLocalization();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}