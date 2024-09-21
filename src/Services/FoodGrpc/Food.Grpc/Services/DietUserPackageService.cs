using System.Text.Json.Serialization;
using AutoMapper;
using Food.Application.Common.Interfaces.Persistence.Repositories;
using Food.Application.Common.Interfaces.Services;
using Food.Application.Common.Specification;
using Food.Domain.Entities;
using Food.Domain.Enum;
using Food.Grpc.Services.Query;
using Grpc.Core;
using MediatR;
using Newtonsoft.Json;
using static Food.Grpc.DietPacks;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace Food.Grpc.Services;

public class DietUserPackageService : DietPacksBase
{

    private readonly IMediator _mediator;
    public DietUserPackageService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<ResonseDietPack> DietUserPackage(RequestGetDietPack request, ServerCallContext context)
    {

       return await _mediator.Send(new GetDietUserPackageQuery() { DietPacks = request }, context.CancellationToken);
    }

}