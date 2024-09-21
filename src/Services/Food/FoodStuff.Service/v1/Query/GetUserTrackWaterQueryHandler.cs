using Common;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query
{
    //public class GetUserTrackWaterQueryHandler : IRequestHandler<GetUserTrackWaterQuery, UserTrackWater>
    //{
    //    private IUserTrackWaterRepository _repository;
    //    public GetUserTrackWaterQueryHandler(IUserTrackWaterRepository repository)
    //    {
    //        _repository = repository;
    //    }

    //    public async Task<UserTrackWater> Handle(GetUserTrackWaterQuery request, CancellationToken cancellationToken)
    //    {
    //      return await _repository.GetTrackWaterAsync(request.dateTime, request.userId, cancellationToken);
    //    }
    //}
}
