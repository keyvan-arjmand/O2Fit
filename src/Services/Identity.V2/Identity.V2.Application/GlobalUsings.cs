// Global using directives

global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Linq.Expressions;
global using System.Net;
global using System.Net.Mail;
global using System.Reflection;
global using System.Text;
global using AutoMapper;
global using FluentValidation;
global using FluentValidation.Results;
global using Identity.V2.Application.Common.ApiResult;
global using Identity.V2.Application.Common.Behaviours;
global using Identity.V2.Application.Common.Constants;
global using Identity.V2.Application.Common.Exceptions;
global using Identity.V2.Application.Common.Interfaces.Persistence.Repositories;
global using Identity.V2.Application.Common.Interfaces.Persistence.UoW;
global using Identity.V2.Application.Common.Interfaces.Services;
global using Identity.V2.Application.Common.Mapping;
global using Identity.V2.Application.Common.Models;
global using Identity.V2.Application.Common.Utilities;
global using Identity.V2.Application.Dtos;
global using Identity.V2.Application.Dtos.CategoryPermissions;
global using Identity.V2.Application.Dtos.Countries;
global using Identity.V2.Application.Dtos.Roles;
global using Identity.V2.Application.Dtos.SpecialDiseases;
global using Identity.V2.Application.Dtos.Users;
global using Identity.V2.Domain.Aggregates.CountryAggregate;
global using Identity.V2.Domain.Aggregates.PermissionCategoryAggregate;
global using Identity.V2.Domain.Aggregates.RoleAggregate;
global using Identity.V2.Domain.Aggregates.SpecialDiseaseAggregate;
global using Identity.V2.Domain.Aggregates.UserAggregate;
global using Identity.V2.Domain.Common;
global using Identity.V2.Domain.Common.User;
global using Identity.V2.Domain.Enums;
global using Identity.V2.Domain.ValueObjects;
global using MassTransit;
global using MediatR;
global using MediatR.Pipeline;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using MongoDB.Bson;
global using MongoDB.Driver;
global using Newtonsoft.Json;
