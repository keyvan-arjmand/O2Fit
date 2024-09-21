using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Service.v1.Command;

namespace WorkoutTracker.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UserTrackStepsController : BaseController
    {
        private readonly IUserTrackStepsRepository _userTrackStepsRepository;
        private readonly IBurnedWorkOutCaloriesRepository _burnedWorkOutCaloriesRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserTrackStepsController(IMediator mediator, IMapper mapper,
            IUserTrackStepsRepository userTrackStepsRepository, IBurnedWorkOutCaloriesRepository burnedWorkOutCaloriesRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userTrackStepsRepository = userTrackStepsRepository;
            _burnedWorkOutCaloriesRepository = burnedWorkOutCaloriesRepository;
        }

        [HttpGet]
        public async Task<List<UserTrackSteps>> GetAsync(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            List<UserTrackSteps> userTrackSteps = await _userTrackStepsRepository.GetList(userId, dateTime, cancellationToken);
            return userTrackSteps;
        }


        [HttpGet("GetAsyncBydate")]
        public async Task<List<UserTrackSteps>> GetAsyncBydate(int userId, int dayscount, CancellationToken cancellationToken)
        {
            List<UserTrackSteps> userTrackSteps = await _userTrackStepsRepository.GetAsyncByDate(userId, dayscount, cancellationToken);
            return userTrackSteps;
        }


        [HttpPost]
        public async Task<ApiResult<UserTrackSteps>> CreateAsync(UserTrackStepsDTO userTrackStepsDTO, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateUserTrackStepsCommand
            {
                Duration = userTrackStepsDTO.Duration,
                InsertDate = userTrackStepsDTO.InsertDate,
                IsManual = userTrackStepsDTO.IsManual,
                StepsCount = userTrackStepsDTO.StepsCount,
                UserId = userTrackStepsDTO.UserId,
                _id = userTrackStepsDTO._id,
                UserWeight = userTrackStepsDTO.UserWeight
            });

            return result;
        }


        [HttpPut("id")]
        public async Task<ApiResult> Edit(UpdateUserTrackStepsDTO updateUserTrackStepsDTO, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateUserTrackStepsCommand
            {
                Duration = updateUserTrackStepsDTO.Duration,
                Id = updateUserTrackStepsDTO.Id,
                InsertDate = updateUserTrackStepsDTO.InsertDate,
                IsManual = updateUserTrackStepsDTO.IsManual,
                StepsCount = updateUserTrackStepsDTO.StepsCount,
                UserId = updateUserTrackStepsDTO.UserId,
                UserWeight = updateUserTrackStepsDTO.UserWeight,
                _id = updateUserTrackStepsDTO._id,
            });
            return Ok();
        }



        [HttpDelete]
        public async Task<ApiResult> Delete(int userTrackStepsId, CancellationToken cancellationToken)
        {
            var userTrackSteps = await _userTrackStepsRepository.GetByIdAsync(cancellationToken, userTrackStepsId);
            await _userTrackStepsRepository.DeleteAsync(userTrackSteps, cancellationToken);
            //-------------------------------Add in BurnedCalorieTrack----------------------
            var _burnedWorkOutCalories = await _burnedWorkOutCaloriesRepository.GetByDate(userTrackSteps.UserId,
                userTrackSteps.InsertDate, cancellationToken);

            var burnedWorkOutCalories = new BurnedWorkOutCalories()
            {
                Id = _burnedWorkOutCalories.Id,
                InsertDate = userTrackSteps.InsertDate,
                UserId = userTrackSteps.UserId,
                _id = _burnedWorkOutCalories._id
            };
            burnedWorkOutCalories.Value = _burnedWorkOutCalories.Value - userTrackSteps.BurnedCalories;
            await _burnedWorkOutCaloriesRepository.UpdateAsync(burnedWorkOutCalories, cancellationToken);
            return Ok();
        }



        [HttpGet("UserHistory")]
        public async Task<List<UserTrackSteps>> GetUserHistory(int userId, int days, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.Now.AddDays(-days);
            //var _userTrackSteps = _userTrackStepsRepository.TableNoTracking.Where(s => s.InsertDate >= dateTime && s.UserId == userId);
            var _userTrackSteps = await (from userSteps in _userTrackStepsRepository.TableNoTracking
                                         where (userSteps.InsertDate >= dateTime) && (userSteps.UserId == userId)
                                         select userSteps).ToListAsync(cancellationToken);
            List<UserTrackSteps> userTrackSteps = (_userTrackSteps.Count() > 0) ? _userTrackSteps.ToList() : null;
            return userTrackSteps;
        }
    }
}
