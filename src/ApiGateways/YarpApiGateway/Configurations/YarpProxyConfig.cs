using System.Security.Authentication;
using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.LoadBalancing;

namespace YarpApiGateway.Configurations;

public class YarpProxyConfig
{
    public static List<RouteConfig> Routes = new List<RouteConfig>()
    {
        new ()
        {
            RouteId = "IdentityRoute",
            ClusterId = "IdentityCluster",
            Match = new RouteMatch()
           {
               Path = "identity/{**catch-all}"
           },
           Transforms = new List<IReadOnlyDictionary<string, string>>
           {
               new Dictionary<string, string>
               {
                   { "PathRemovePrefix", "/identity" }
               }
           }
        },
        new()
        {
            RouteId = "NotificationRoute",
            ClusterId = "NotificationCluster",
            Match = new RouteMatch()
            {
                Path = "notification/{**catch-all}"
            },
            Transforms = new List<IReadOnlyDictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    { "PathRemovePrefix", "/notification" }
                }
            }
        }
        // new()
        // {
        //     RouteId = "ipChallengeRoute",
        //     ClusterId = "ipChallengeCluster",
        //     Match = new RouteMatch()
        //     {
        //         Path = "ipChallenge/{**catch-all}"
        //     },
        //     Transforms = new List<IReadOnlyDictionary<string, string>>
        //     {
        //         new Dictionary<string, string>
        //         {
        //             { "PathRemovePrefix", "/ipChallenge" }
        //         }
        //     }
        // }
    };

    public static List<ClusterConfig> Clusters = new List<ClusterConfig>()
    {
        new()
        {
            ClusterId = "IdentityCluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "destination1", new DestinationConfig { Address = "http://host.docker.internal:5012/" } },
            }
           // HttpClient = new HttpClientConfig { MaxConnectionsPerServer = 10, SslProtocols = SslProtocols.Tls11
           //     | SslProtocols.Tls12 }
        },
        new()
        {
            ClusterId = "NotificationCluster",
            LoadBalancingPolicy = LoadBalancingPolicies.RoundRobin,
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "destination1", new DestinationConfig { Address = "http://host.docker.internal:5032/" } },
                { "destination2", new DestinationConfig { Address = "http://host.docker.internal:7032/" } }
            },
            HttpClient = new HttpClientConfig
            {
                MaxConnectionsPerServer = 10, SslProtocols = SslProtocols.Tls11
                                                             | SslProtocols.Tls12
            }
        },
        // new ()
        // {
        //     ClusterId = "ipChallengeCluster",
        //     Destinations = new Dictionary<string, DestinationConfig>
        //     {
        //         { "destination1", new DestinationConfig { Address = "https://www.idenv2test.o2fitt.com/" } },
        //     },
        //     HttpClient = new HttpClientConfig { MaxConnectionsPerServer = 10, SslProtocols = SslProtocols.Tls11
        //         | SslProtocols.Tls12 }
        // }
        //ipChallengeCluster
    };
};