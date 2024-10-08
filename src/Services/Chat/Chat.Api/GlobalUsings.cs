﻿// Global using directives

global using System.Net;
global using System.Security.Claims;
global using AutoMapper;
global using Chat.Api;
global using Chat.Api.Filters;
global using Chat.Api.Hubs;
global using Chat.Api.Middlewares;
global using Chat.Api.Permission;
global using Chat.Api.Services;
global using Chat.Application;
global using Chat.Application.Common.ApiResult;
global using Chat.Application.Common.Exceptions;
global using Chat.Application.Common.Interfaces.Services;
global using Chat.Application.Common.Mapping;
global using Chat.Domain.Common;
global using Chat.Infrastructure;
global using Chat.Infrastructure.Common;
global using Common.Constants.Identity;
global using FluentValidation.AspNetCore;
global using MassTransit;
global using MediatR;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.AspNetCore.SignalR;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Logging;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using MongoDB.Bson;
global using OpenIddict.Validation.AspNetCore;
global using Swashbuckle.AspNetCore.SwaggerGen;