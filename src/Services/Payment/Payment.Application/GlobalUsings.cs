// Global using directives

global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Linq.Expressions;
global using System.Net;
global using System.Reflection;
global using AutoMapper;
global using FluentValidation;
global using FluentValidation.Results;
global using MediatR;
global using MediatR.Pipeline;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using MongoDB.Bson;
global using MongoDB.Driver;
global using Newtonsoft.Json;
global using Payment.Application.Common.ApiResult;
global using Payment.Application.Common.Behaviours;
global using Payment.Application.Common.Interfaces.Persistence.Repositories;
global using Payment.Application.Common.Models;
global using Payment.Application.Common.Utilities;
global using Payment.Domain.Common;
global using ValidationException = Payment.Application.Common.Exceptions.ValidationException;