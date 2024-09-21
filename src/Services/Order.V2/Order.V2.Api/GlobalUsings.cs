// Global using directives

global using System.Net;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
global using AutoMapper;
global using Common.Constants.Identity;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using OpenIddict.Validation.AspNetCore;
global using Order.V2.Api;
global using Order.V2.Api.Middlewares;
global using Order.V2.Api.Permission;
global using Order.V2.Application;
global using Order.V2.Application.Common.ApiResult;
global using Order.V2.Application.Common.Exceptions;
global using Order.V2.Application.Common.Interfaces.Services;
global using Order.V2.Application.Common.Mapping;
global using Order.V2.Infrastructure;
global using Order.V2.Infrastructure.Common;
global using Swashbuckle.AspNetCore.SwaggerGen;