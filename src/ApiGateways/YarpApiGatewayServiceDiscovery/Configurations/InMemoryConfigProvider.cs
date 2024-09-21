namespace YarpApiGatewayServiceDiscovery.Configurations
{
    public class InMemoryConfigProvider : IProxyConfigProvider, IHostedService, IDisposable
    {
        private Timer _timer;
        private volatile InMemoryConfig _config;
        private readonly DiscoveryClient _discoveryClient;
        private readonly RouteConfig[] _routes;

        public InMemoryConfigProvider(IDiscoveryClient discoveryClient)
        {
            _discoveryClient = discoveryClient as DiscoveryClient;

            _routes = new[]
            {
                new RouteConfig()
                {
                    RouteId = "notification_route",
                    ClusterId = "NOTIFICATION.API",
                    Match = new RouteMatch
                    {
                        Path = "notifications/{**catchall}"
                    },
                    Transforms = new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>
                        {
                            { "PathPattern", "{**catchall}" }
                        }
                    }
                },
                new RouteConfig()
                {
                    RouteId = "identity_route",
                    ClusterId = "IDENTITY.V2.API",
                    Match = new RouteMatch
                    {
                        Path = "identity/{**catchall}"
                    },
                    Transforms = new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>
                        {
                            { "PathPattern", "{**catchall}" }
                        }
                    }
                }
            };

            PopulateConfig();
        }

        private void Update(object state)
        {
            PopulateConfig();
        }

        private void PopulateConfig()
        {
            var apps = _discoveryClient.Applications.GetRegisteredApplications();
            List<ClusterConfig> clusters = new();

            foreach (var app in apps)
            {
                var cluster = new ClusterConfig
                {
                    LoadBalancingPolicy = "RoundRobin",
                    ClusterId = app.Name,
                    Destinations = app.Instances
                        .Select(x =>
                            (x.InstanceId,
                                new DestinationConfig()
                                {
                                    //Address = $"https://{x.HostName}:{x.SecurePort}"
                                    Address = $"http://{x.HostName}:{x.Port}"
                                }))
                        .ToDictionary(y => y.InstanceId, y => y.Item2)
                };

                clusters.Add(cluster);
            }

            var oldConfig = _config;
            _config = new InMemoryConfig(_routes, clusters);
            oldConfig?.SignalChange();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }


        public IProxyConfig GetConfig() => _config;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Update, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }


        private class InMemoryConfig : IProxyConfig
        {
            private readonly CancellationTokenSource _cts = new CancellationTokenSource();

            public InMemoryConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
            {
                Routes = routes;
                Clusters = clusters;
                ChangeToken = new CancellationChangeToken(_cts.Token);
            }


            public IReadOnlyList<RouteConfig> Routes { get; }

            public IReadOnlyList<ClusterConfig> Clusters { get; }

            public IChangeToken ChangeToken { get; }

            internal void SignalChange()
            {
                _cts.Cancel();
            }
        }
    }
}