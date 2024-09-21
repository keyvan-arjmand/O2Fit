﻿// Global using directives

global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Linq.Expressions;
global using System.Reflection;
global using AutoMapper;
global using FluentValidation;
global using MediatR;
global using MediatR.Pipeline;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using MongoDB.Bson;
global using MongoDB.Driver;
global using Newtonsoft.Json;
global using Workout.V2.Application.Common.Behaviours;
global using Workout.V2.Application.Common.Constants;
global using Workout.V2.Application.Common.Exceptions;
global using Workout.V2.Application.Common.Interfaces.Persistence.Repositories;
global using Workout.V2.Application.Common.Interfaces.Persistence.UoW;
global using Workout.V2.Application.Common.Interfaces.Services;
global using Workout.V2.Application.Common.Mapping;
global using Workout.V2.Application.Common.Models;
global using Workout.V2.Application.Common.Utilities;
global using Workout.V2.Application.Dtos.BodyMuscles;
global using Workout.V2.Application.Dtos.Workouts;
global using Workout.V2.Domain.Aggregates.BodyMusclesAggregate;
global using Workout.V2.Domain.Aggregates.WorkoutsAggregate;
global using Workout.V2.Domain.Common;
global using Workout.V2.Domain.Enums;