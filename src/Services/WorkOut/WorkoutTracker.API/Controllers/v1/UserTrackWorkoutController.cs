using System;
using System.Collections.Generic;
using Common.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Data.Repositories;
using WorkoutTracker.Domain.Entities.WorkOut;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.v1.Query;
using WorkoutTracker.Service.v1.Command.CreatUserTrackWorkout;

namespace WorkoutTracker.API.Controllers.v1
{
    [AllowAnonymous]
    [ApiVersion("1")]
    public class UserTrackWorkoutController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserTrackWorkOutRepository _userTrackWorkOutRepository;
        private readonly IRepository<PersonalWorkOut> _personalWorkoutRepository;
        private readonly IRepository<WorkOut> _workoutRepository;
        private readonly IWorkoutAttributeRepository _workoutAttributeRepository;
        private readonly IRepository<WorkOutAttributeValue> _workOutAttributeValueRepository;
        private readonly IBurnedWorkOutCaloriesRepository _burnedWorkOutCaloriesRepository;
        public UserTrackWorkoutController(IUserTrackWorkOutRepository userTrackWorkOutRepository,
            IRepository<WorkOutAttributeValue> workOutAttributeValueRepository,
            IWorkoutAttributeRepository workoutAttributeRepository,
            IBurnedWorkOutCaloriesRepository burnedWorkOutCaloriesRepository,
            IMediator mediator, IMapper mapper, IRepository<PersonalWorkOut> personalWorkoutRepository, IRepository<WorkOut> workoutRepository
            )
        {
            _mapper = mapper;
            _mediator = mediator;
            _userTrackWorkOutRepository = userTrackWorkOutRepository;
            _workOutAttributeValueRepository = workOutAttributeValueRepository;
            _workoutAttributeRepository = workoutAttributeRepository;
            _burnedWorkOutCaloriesRepository = burnedWorkOutCaloriesRepository;
            _personalWorkoutRepository = personalWorkoutRepository;
            _personalWorkoutRepository = personalWorkoutRepository;
            _workoutRepository = workoutRepository;
        }

        [HttpGet]
        public async Task<ApiResult<UserTrackWorkOut>> Get(int userTrackWorkId, CancellationToken cancellationToken)
        {
            var userTrackWorkout = await _userTrackWorkOutRepository.GetByIdAsync(cancellationToken, userTrackWorkId);

            return userTrackWorkout;
        }

        [HttpGet("GetByDate")]
        public async Task<ApiResult<List<UserTrackWorkOutModelDTO>>> GetByDate(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var userTrackWorkouts = await _userTrackWorkOutRepository.GetByDate(userId, dateTime, cancellationToken);

            var userTrackWorkoutList = new List<UserTrackWorkOutModelDTO>();
            foreach (var item in userTrackWorkouts)
            {
                var userTrackWorkout = new UserTrackWorkOutModelDTO();

                userTrackWorkout.Id = item.Id;
                userTrackWorkout.BurnedCalories = item.BurnedCalories;
                userTrackWorkout.Duration = item.Duration;
                userTrackWorkout.InsertDate = item.InsertDate;
                userTrackWorkout._id = item._id;
                userTrackWorkout.PersonalWorkOutId = item.PersonalWorkOutId;
                userTrackWorkout.WorkOutAttributeValueId = item.WorkOutAttributeValueId;
                userTrackWorkout.WorkOutId = item.WorkOutId;
                userTrackWorkout.UserId = item.UserId;
                if (item.WorkOutId > 0)
                {
                    userTrackWorkout.Classification = item.WorkOut.Classification;
                }

                userTrackWorkoutList.Add(userTrackWorkout);
            }
            return userTrackWorkoutList;
        }

        [HttpGet("GetFullByDate")]
        public async Task<ApiResult<List<UserTrackWorkOutSelectDTO>>> GetFullByDate(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var userTrackWorkouts = await _userTrackWorkOutRepository.GetByDate(userId, dateTime, cancellationToken);
            var translatinIds = new List<int>();
            foreach (var item in userTrackWorkouts)
            {
                //afsane
                if (item.WorkOut != null)
                {
                    translatinIds.Add(item.WorkOut.NameId);
                }
            }
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = translatinIds, Language = LanguageName });
            var userTrackWorkoutList = new List<UserTrackWorkOutSelectDTO>();
            foreach (var item in userTrackWorkouts)
            {
                var userTrackWorkout = new UserTrackWorkOutSelectDTO()
                {
                    BurnedCalories = item.BurnedCalories,
                    Duration = item.Duration,
                    InsertDate = item.InsertDate,
                    _id = item._id,
                    //afsane
                    workOut = new SelectOption<int>
                    {
                        Text = (item.WorkOut != null) ? names.Find(n => n.Value == item.WorkOut.NameId).Text :
                    item.PersonalWorkOut.Name
                    ,
                        Value = item.Id
                    },
                    UserId = item.UserId
                };
                userTrackWorkoutList.Add(userTrackWorkout);
            }
            return userTrackWorkoutList;
        }


        [HttpPost]
        public async Task<ApiResult<CreatUserTrackWorkoutResult>> Post(UserTrackWorkOutDTO userTrackWorkOutDTO, CancellationToken cancellationToken)
        {
            UserTrackWorkOut userTrackWorkOut = userTrackWorkOutDTO.ToEntity(_mapper);
            userTrackWorkOut.WorkOutAttributeValueId = userTrackWorkOutDTO.WorkOutAttributeValueId;
            userTrackWorkOut._id = userTrackWorkOutDTO._id;
            var duration = userTrackWorkOutDTO.Duration.TotalMinutes;

            return await _mediator.Send(new CreatUserTrackWorkoutCommand
            {
                duration = duration,
                userTrackWorkOut = userTrackWorkOut,
                Weight = userTrackWorkOutDTO.Weight,
            });
        }

        [HttpPut]
        public async Task<ApiResult<UserTrackWorkOutModelDTO>> Put(UserTrackWorkOutDTO userTrackWorkOutDTO, CancellationToken cancellationToken)
        {
            UserTrackWorkOut OldUserTrackWorkOut = await _userTrackWorkOutRepository.Table
                    .Include(a => a.WorkOut)
                    .Include(a => a.WorkOutAttributeValue)
                    .Include(a => a.PersonalWorkOut)
                    .SingleOrDefaultAsync(a => a.Id == userTrackWorkOutDTO.Id);

            _userTrackWorkOutRepository.Detach(OldUserTrackWorkOut);

            UserTrackWorkOut userTrackWorkOut = userTrackWorkOutDTO.ToEntity(_mapper);
            var duration = userTrackWorkOutDTO.Duration.TotalMinutes;

            if (userTrackWorkOutDTO.WorkOutId != null)
            {
                if (userTrackWorkOutDTO.WorkOutAttributeValueId == null)
                {
                    userTrackWorkOut.BurnedCalories = userTrackWorkOutDTO.Weight * duration * OldUserTrackWorkOut.WorkOut.BurnedCalories;
                }
                else
                {
                    userTrackWorkOut.BurnedCalories = userTrackWorkOutDTO.Weight * duration * OldUserTrackWorkOut.WorkOutAttributeValue.BurnedCalories;
                }
            }
            else
            {
                userTrackWorkOut.BurnedCalories = duration * OldUserTrackWorkOut.PersonalWorkOut.Calorie / OldUserTrackWorkOut.PersonalWorkOut.Duration.TotalMinutes;
            }

            await _userTrackWorkOutRepository.UpdateAsync(userTrackWorkOut, cancellationToken);

            //-------------------------------update in BurnedCalorieTrack----------------------
            var _burnedWorkOutCalories = await _burnedWorkOutCaloriesRepository.GetByDate(userTrackWorkOut.UserId, userTrackWorkOutDTO.InsertDate, cancellationToken);
            var burnedWorkOutCalories = new BurnedWorkOutCalories()
            {
                InsertDate = userTrackWorkOut.InsertDate,
                UserId = userTrackWorkOut.UserId,
                Id = _burnedWorkOutCalories.Id,
                _id = _burnedWorkOutCalories._id,
                Value = _burnedWorkOutCalories.Value - OldUserTrackWorkOut.BurnedCalories + userTrackWorkOut.BurnedCalories
            };
            await _burnedWorkOutCaloriesRepository.UpdateAsync(burnedWorkOutCalories, cancellationToken);
            //---------------------------------------------------------------------------------------------------
            UserTrackWorkOutModelDTO result = new UserTrackWorkOutModelDTO()
            {
                BurnedCalories = userTrackWorkOut.BurnedCalories,
                Id = userTrackWorkOut.Id,
                _id = userTrackWorkOut._id,
                PersonalWorkOutId = userTrackWorkOut.PersonalWorkOutId,
                UserId = userTrackWorkOut.UserId,
                Duration = userTrackWorkOut.Duration,
                InsertDate = userTrackWorkOut.InsertDate,
                WorkOutAttributeValueId = userTrackWorkOut.WorkOutAttributeValueId,
                WorkOutId = userTrackWorkOut.WorkOutId
            };
            return result;
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int userTrackWorkoutId, CancellationToken cancellationToken)
        {
            var userTrackWorkout = await _userTrackWorkOutRepository.GetByIdAsync(cancellationToken, userTrackWorkoutId);
            await _userTrackWorkOutRepository.DeleteAsync(userTrackWorkout, cancellationToken);
            //-------------------------------Add in BurnedCalorieTrack----------------------
            var _burnedWorkOutCalories = await _burnedWorkOutCaloriesRepository.GetByDate(userTrackWorkout.UserId, userTrackWorkout.InsertDate, cancellationToken);
            var burnedWorkOutCalories = new BurnedWorkOutCalories()
            {
                InsertDate = userTrackWorkout.InsertDate,
                UserId = userTrackWorkout.UserId,
                _id = _burnedWorkOutCalories._id
            };
            burnedWorkOutCalories.Value = burnedWorkOutCalories.Value - userTrackWorkout.BurnedCalories;
            await _burnedWorkOutCaloriesRepository.UpdateAsync(burnedWorkOutCalories, cancellationToken);
            return Ok();
        }


        [HttpGet("GetBurnedCalori")]
        public async Task<int> GetBurnedCalori(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var burnedCalories = await _burnedWorkOutCaloriesRepository.GetByDate(userId, dateTime, cancellationToken);
            return Convert.ToInt32(burnedCalories.Value);
        }


        [HttpGet("UserHistory")]
        public async Task<ApiResult<List<UserTrackWorkOutModelDTO>>> GetUserHistory(int userId, int days, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.Now.AddDays(-days);
            var userTrackWorkouts = await _userTrackWorkOutRepository.GetUserHistory(userId, dateTime, cancellationToken);

            var userTrackWorkoutList = new List<UserTrackWorkOutModelDTO>();
            foreach (var item in userTrackWorkouts)
            {
                var userTrackWorkout = new UserTrackWorkOutModelDTO();

                userTrackWorkout.Id = item.Id;
                userTrackWorkout.BurnedCalories = item.BurnedCalories;
                userTrackWorkout.Duration = item.Duration.Duration();
                userTrackWorkout.InsertDate = item.InsertDate;
                userTrackWorkout._id = item._id;
                userTrackWorkout.PersonalWorkOutId = item.PersonalWorkOutId;
                userTrackWorkout.WorkOutAttributeValueId = item.WorkOutAttributeValueId;
                userTrackWorkout.WorkOutId = item.WorkOutId;
                userTrackWorkout.UserId = item.UserId;
                if (item.WorkOutId > 0)
                {
                    userTrackWorkout.Classification = item.WorkOut.Classification;
                }

                userTrackWorkoutList.Add(userTrackWorkout);
            }
            return userTrackWorkoutList;
        }

    }
}