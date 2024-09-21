using Microsoft.AspNetCore.Builder;
using System.Security.Authentication;
using System.Text;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.LoadBalancing;
using Yarp.ReverseProxy.Transforms;
using YarpApiGateway.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.WebHost.ConfigureKestrel(cfg => cfg.RequestHeaderEncodingSelector = _ => Encoding.Latin1);

builder.Services.AddReverseProxy().LoadFromMemory(YarpProxyConfig.Routes, YarpProxyConfig.Clusters);
//.AddTransforms(x =>
//{
//    x.AddForwarded();
//});

var app = builder.Build();
app.UseRouting();
app.MapReverseProxy();

// Configure the HTTP request pipeline.


app.Run();
