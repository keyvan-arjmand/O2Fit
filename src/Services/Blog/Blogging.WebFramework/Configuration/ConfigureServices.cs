using System.Reflection;
using Data.Contracts;
using Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace WebFramework.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}