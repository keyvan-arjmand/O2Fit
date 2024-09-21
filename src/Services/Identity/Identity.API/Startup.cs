using Common;
using Identity.Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.Middlewares;
using WebFramework.Swagger;

namespace Identity.API
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
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddCustomCors();

            services.InitializeAutoMapper();

            services.AddDbContext(Configuration);

            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.AddMinimalMvc();

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddCustomRefit();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSwagger();

            services.AddLocalizations();

            services.AddRedis(Configuration);

            services.AddCustomRedis(_siteSetting.RedisClient);

            services.AddCustomDapper(Configuration);

            services.AddCustomIdentityServer(Configuration);

            #region Refit
            string token = "";
            if (request != null)
            {
                token = request.Cookies["mytoken"].ToString();
            }
            services.AddRefitClient<IUserService>(
                new RefitSettings
                {
                    AuthorizationHeaderValueGetter = () => Task.FromResult(token)
                })
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://user.o2fitt.com"));
            //.ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:44314"));

            #endregion

            return services.BuildAutofacServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();

            app.UseHsts(env);

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseSwaggerAndUI();
            }

            app.UseRouting();

            app.UseIdentityServer();

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
