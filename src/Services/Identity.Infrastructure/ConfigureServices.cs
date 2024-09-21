using Identity.Application.Common.Interface;
using Identity.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // services.AddScoped<IUserRepository, UserRepository>();
        // services.AddScoped<IUserCeremonyRepository, UserCeremonyRepository>();
        // services.AddScoped<ICeremonyRepository, CeremonyRepository>();
        // services.AddScoped<IinvitedUserRepository, InvitedUserRepository>();
        // services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        // services.AddScoped<IStateRepository, StateRepository>();
        // services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();



        return services;
    }
}