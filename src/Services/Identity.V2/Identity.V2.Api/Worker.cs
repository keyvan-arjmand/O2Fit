namespace Identity.V2.Api;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    
    public Worker(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        await CreateApplicationsAsync(scope, _configuration).ConfigureAwait(false);
        await CreateScopesAsync(scope, _configuration).ConfigureAwait(false);
        
        async Task CreateApplicationsAsync(AsyncServiceScope scope, IConfiguration builder)
        {
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("release_v2") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "release_v2",
                    ClientSecret = "D0A32D54-4EC6-46D7-A2B7-804157443129",
                    RedirectUris =
                    {
                        new Uri("https://localhost:6022/")
                    },
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:6022/")
                    },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                        OpenIddictConstants.Permissions.ResponseTypes.Code,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Prefixes.Scope + "api2"
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange,
                    }
                });
            }

            if (await manager.FindByClientIdAsync("auth_server9744F0F8-ABF8-4512-BA6F-D1E88825BA4C") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "auth_server9744F0F8-ABF8-4512-BA6F-D1E88825BA4C",
                    ClientSecret = "5F128CDE-45F3-4261-A452-B443702DE6D7",
                    //ConsentType = ConsentTypes.Implicit,
                    //Type = ClientTypes.Public,
                    RedirectUris =
                    {
                        new Uri(
                            _configuration["AuthServerLocalhostSettings:ClientUrls:OrderV2:RedirectUrl"]!)
                    },
                    PostLogoutRedirectUris =
                    {
                        new Uri(_configuration["AuthServerLocalhostSettings:ClientUrls:OrderV2:LogoutUrl"]!)
                    },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                        OpenIddictConstants.Permissions.ResponseTypes.Code,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:OrderV2:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Payment:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Currency:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Discount:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Wallet:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Track:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Food:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Notification:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Ticket:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Nutritionist:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Advertise:Name"]
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange,
                    }
                });
            }

            if (await manager.FindByClientIdAsync("farzam", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "farzam",
                    ClientSecret = "farzam123",
                    //ConsentType = ConsentTypes.Implicit,
                    //Type = OpenIddictConstants.ClientTypes.Public,
                    //RedirectUris =
                    //{
                    //    new Uri("https://workout1.o2fitt.com/")
                    //},
                    //PostLogoutRedirectUris =
                    //{
                    //    new Uri("https://workout1.o2fitt.com/")
                    //},
                    Permissions =
                    {
                        //Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        //Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                        OpenIddictConstants.Permissions.ResponseTypes.Code,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:OrderV2:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Payment:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Currency:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Discount:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Wallet:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Food:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Track:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Notification:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Ticket:Name"]
                    }
                    //Requirements =
                    //{
                    //    Requirements.Features.ProofKeyForCodeExchange,
                    //}
                }, cancellationToken);
            }

            if (await manager.FindByClientIdAsync("farzam2", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "farzam2",
                    ClientSecret = "farzam123",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                        OpenIddictConstants.Permissions.ResponseTypes.Code,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:OrderV2:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Payment:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Currency:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Discount:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Wallet:Name"],
                        OpenIddictConstants.Permissions.Prefixes.Scope +
                        _configuration["AuthServerLocalhostSettings:Scopes:Food:Name"]
                    },
                    Requirements =
                    {
                        OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange,
                    }
                }, cancellationToken);
            }

            if (await manager.FindByClientIdAsync("release_v3", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "release_v3",
                    ClientSecret = "12345678",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token,

                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                        OpenIddictConstants.Permissions.ResponseTypes.Code,

                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Prefixes.Scope + "api2"
                    }
                }, cancellationToken);
            }
        }

        // Note: no client registration is created for resource_server_2
        // as it uses local token validation instead of introspection.
        async Task CreateScopesAsync(AsyncServiceScope scopeService, IConfiguration builder)
        {
            var manager = scopeService.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();


            if (await manager.FindByNameAsync("api1", cancellationToken).ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = "api1",
                    Resources =
                    {
                        "test_api_server"
                    }
                }, cancellationToken).ConfigureAwait(false);
            }

          
            //
            if (await manager.FindByNameAsync("api2", cancellationToken).ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = "api2",
                    Resources =
                    {
                        "test_resource_server"
                    }
                }, cancellationToken).ConfigureAwait(false);
            }

            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:OrderV2:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:OrderV2:Name"],
                    Resources =
                    {
                        _configuration[
                            "AuthServerLocalhostSettings:Scopes:OrderV2:Resources:OrderV2Resource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }

            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Payment:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Payment:Name"],
                    Resources =
                    {
                        _configuration[
                            "AuthServerLocalhostSettings:Scopes:Payment:Resources:PaymentResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }

            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Currency:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Currency:Name"],
                    Resources =
                    {
                        _configuration[
                            "AuthServerLocalhostSettings:Scopes:Currency:Resources:CurrencyResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }

            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Discount:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Discount:Name"],
                    Resources =
                    {
                        _configuration[
                            "AuthServerLocalhostSettings:Scopes:Discount:Resources:DiscountResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }

            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Wallet:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Wallet:Name"],
                    Resources =
                    {
                        _configuration["AuthServerLocalhostSettings:Scopes:Wallet:Resources:WalletResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }

            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Track:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Track:Name"],
                    Resources =
                    {
                        _configuration["AuthServerLocalhostSettings:Scopes:Track:Resources:TrackResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }

            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Food:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Food:Name"],
                    Resources =
                    {
                        _configuration["AuthServerLocalhostSettings:Scopes:Food:Resources:FoodResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }
            
            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Notification:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Notification:Name"],
                    Resources =
                    {
                        _configuration["AuthServerLocalhostSettings:Scopes:Notification:Resources:NotificationResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }
            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Ticket:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Ticket:Name"],
                    Resources =
                    {
                        _configuration["AuthServerLocalhostSettings:Scopes:Ticket:Resources:TicketResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }
            
            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Nutritionist:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Nutritionist:Name"],
                    Resources =
                    {
                        _configuration["AuthServerLocalhostSettings:Scopes:Nutritionist:Resources:NutritionistResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }
            
            
            if (await manager
                    .FindByNameAsync(_configuration["AuthServerLocalhostSettings:Scopes:Advertise:Name"]!, cancellationToken)
                    .ConfigureAwait(false) is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = _configuration["AuthServerLocalhostSettings:Scopes:Advertise:Name"],
                    Resources =
                    {
                        _configuration["AuthServerLocalhostSettings:Scopes:Advertise:Resources:AdvertiseResource"]!
                    }
                }, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}