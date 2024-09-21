using Microsoft.Extensions.DependencyInjection;
using Domain;
using Common;
using Microsoft.AspNetCore.Identity;
using Data;
using Data.Database;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace WebFramework.Configuration
{
    public static class IdentityConfigurationExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddIdentity<User, Role>(identityOptions =>
            {
                //Password Settings
                identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumic; //#@!
                identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

                //UserName Settings
                identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;

                //Singin Settings
                identityOptions.SignIn.RequireConfirmedEmail = false;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

                //Lockout Settings
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                identityOptions.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }

        //Identity Server4
        public static void AddCustomIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServer()
             .AddDeveloperSigningCredential()
             //.AddSigningCredential()
             .AddConfigurationStore(options =>
             {
                 options.ConfigureDbContext = builder => builder.UseNpgsql(configuration.GetConnectionString("Npgsql"));
             })
             .AddOperationalStore(options =>
             {
                 options.ConfigureDbContext = builder => builder.UseNpgsql(configuration.GetConnectionString("Npgsql"));
                 options.EnableTokenCleanup = true;
                 options.TokenCleanupInterval = 30; // interval in seconds
              })
             .AddAspNetIdentity<User>();
            //.AddConfigurationStoreCache();
        }
    }
}
