using AutoMapper;
using FoodStuff.API.Models;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UserTrackWaterController : BaseController
    {
        private readonly IUserTrackWaterRepository _userTrackWaterrepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserTrackWaterController(IUserTrackWaterRepository userTrackWaterRepository, IMediator mediator, IMapper mapper)
        {
            _userTrackWaterrepository = userTrackWaterRepository;
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ApiResult<List<UserTrackWater>>> GetAsync(DateTime? StartDate, DateTime? EndDate, int userId
            , CancellationToken cancellationToken)
        {
            return await _userTrackWaterrepository.GetTrackWaterAsync(StartDate, EndDate, userId, cancellationToken);
        }

        [HttpPost]
        public async Task<ApiResult<UserTrackWater>> SetAsync(UserTrackWaterDTO userTrackWaterdto, CancellationToken cancellationToken)
        {
            return await _userTrackWaterrepository.SetTrackWaterAsync(userTrackWaterdto.InsertDate, userTrackWaterdto.UserId,
                  userTrackWaterdto.Value, userTrackWaterdto._id, cancellationToken);
        }

        [HttpGet("UserHistory")]
        public async Task<ApiResult<List<UserTrackWater>>> GetUserHistory(int userId, int days
           , CancellationToken cancellationToken)
        {
            return await _userTrackWaterrepository.GetUserTrackWaterHistory(userId, DateTime.Now.AddDays(-days), cancellationToken);
        }

    }
}
