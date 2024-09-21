// Global using directives

global using System.Collections;
global using System.Linq.Expressions;
global using System.Text;
global using System.Text.Json;
global using Discount.Application.Common.Interfaces.Persistence.Repositories;
global using Discount.Application.Common.Interfaces.Persistence.UoW;
global using Discount.Application.Common.Interfaces.Services;
global using Discount.Application.Common.Models;
global using Discount.Domain.Common;
global using Discount.Infrastructure.Common;
global using Discount.Infrastructure.Persistence;
global using Discount.Infrastructure.Persistence.Repositories;
global using Discount.Infrastructure.Persistence.UoW;
global using Discount.Infrastructure.Services;
global using EventBus.Messages.Events;
global using EventStore.Client;
global using MediatR;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization;
global using MongoDB.Bson.Serialization.Conventions;
global using MongoDB.Bson.Serialization.Serializers;
global using MongoDB.Driver;
global using MongoDB.Driver.Linq;
global using StackExchange.Redis;