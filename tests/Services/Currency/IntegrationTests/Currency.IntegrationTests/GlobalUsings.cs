﻿global using Currency.Application.Common.Interfaces.Persistence.UoW;
global using DotNet.Testcontainers.Builders;
global using MediatR;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using NUnit.Framework;
global using Testcontainers.EventStoreDb;
global using Testcontainers.MongoDb;
global using Testcontainers.RabbitMq;