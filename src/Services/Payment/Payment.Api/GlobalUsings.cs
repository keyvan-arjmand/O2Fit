// Global using directives

global using System.Net;
global using System.Security.Claims;
global using System.Text.Json;
global using AutoMapper;
global using Common.Constants.Identity;
global using FluentValidation.AspNetCore;
global using MassTransit;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using MongoDB.Bson;
global using OpenIddict.Validation.AspNetCore;
global using Payment.Api;
global using Payment.Api.Filters;
global using Payment.Api.Middlewares;
global using Payment.Api.Permission;
global using Payment.Api.Services;
global using Payment.Application;
global using Payment.Application.Common.ApiResult;
global using Payment.Application.Common.Exceptions;
global using Payment.Application.Common.Interfaces.Services;
global using Payment.Application.Common.Mapping;
global using Payment.Domain.Common;
global using Payment.Infrastructure;
global using Payment.Infrastructure.Common;
global using Swashbuckle.AspNetCore.SwaggerGen;