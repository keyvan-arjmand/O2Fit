using Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ordering.Data.Contracts;
using Refit;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.Middlewares;
using WebFramework.Swagger;

namespace Ordering.API
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }
        public HttpRequest request { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddCustomCors();

            services.InitializeAutoMapper();

            services.AddDbContext(Configuration);

            services.AddMinimalMvc();

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddLocalizations();

            services.AddCustomRefit();

            services.AddSwagger();

            services.AddRedis(Configuration);
            services.AddCustomRedis(_siteSetting.RedisClient);
            #region Refit
            string token = "";
            if (request != null)
            {
                token = request.Cookies["mytoken"].ToString();
            }
            services.AddRefitClient<ISocialService>(
                new RefitSettings
                {
                    AuthorizationHeaderValueGetter = () => Task.FromResult(token)
                })
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://social.o2fitt.com"));
            //.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://socialtest.o2fitt.com"));

            #endregion
            return services.BuildAutofacServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCustomExceptionHandler();

            app.UseHsts(env);

            if (env.IsDevelopment())
            {
                app.UseSwaggerAndUI();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
