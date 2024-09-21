// Global using directives

global using System.Collections;
global using System.Linq.Expressions;
global using System.Text.Json;
global using Food.V2.Application.Common.Interfaces.Persistence.Repositories;
global using Food.V2.Application.Common.Interfaces.Persistence.UoW;
global using Food.V2.Application.Common.Interfaces.Services;
global using Food.V2.Application.Common.Models;
global using Food.V2.Domain.Aggregates.DietPackAggregate;
global using Food.V2.Domain.Aggregates.IngredientAggregate;
global using Food.V2.Domain.Common;
global using Food.V2.Infrastructure.Common;
global using Food.V2.Infrastructure.Persistence;
global using Food.V2.Infrastructure.Persistence.Repositories;
global using Food.V2.Infrastructure.Persistence.UoW;
global using Food.V2.Infrastructure.Services;
global using MediatR;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization;
global using MongoDB.Bson.Serialization.Conventions;
global using MongoDB.Bson.Serialization.IdGenerators;
global using MongoDB.Bson.Serialization.Serializers;
global using MongoDB.Driver;
global using MongoDB.Driver.Linq;
global using StackExchange.Redis;
global using JsonSerializer = System.Text.Json.JsonSerializer;