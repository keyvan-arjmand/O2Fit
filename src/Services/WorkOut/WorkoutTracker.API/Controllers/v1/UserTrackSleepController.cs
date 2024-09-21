using AutoMapper;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.API.Models;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;
using WorkoutTracker.Service.Formula;

namespace WorkoutTracker.API.Controllers.v1
{
    [ApiVersion("1")]
    public class UserTrackSleepController : BaseController
    {
        private readonly IUserTrackSleepRepository _UserTrackSleepRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserTrackSleepController(IRepository<UserTrackSleep> repository, IUserTrackSleepRepository UserTrackSleepRepository, IMediator mediator, IMapper mapper)
        {
            _UserTrackSleepRepository = UserTrackSleepRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ApiResult<UserTrackSleepModelDTO>> AddAsync(UserTrackSleepDTO userTrackSleepDTO, CancellationToken cancellationToken)
        {
            System.TimeSpan duration = userTrackSleepDTO.EndDate - userTrackSleepDTO.StartDate;

            UserTrackSleep userTrackSleep = userTrackSleepDTO.ToEntity(_mapper);
            userTrackSleep.InsertDate = userTrackSleepDTO.EndDate;
            userTrackSleep.Duration = duration;
            var bmr = Bmr.CalBmr(userTrackSleepDTO.weight, userTrackSleepDTO.height, userTrackSleepDTO.age, userTrackSleepDTO.gender, null);
            userTrackSleep.BurnedCalories = Convert.ToInt32((bmr / 1440) * duration.TotalMinutes);
            userTrackSleep._id = userTrackSleepDTO._id;
            userTrackSleep.Id = await _UserTrackSleepRepository.AddAsync(userTrackSleep, cancellationToken);

            var result = new UserTrackSleepModelDTO()
            {
                Id = userTrackSleep.Id,
                UserId = userTrackSleep.UserId,
                Rate = userTrackSleep.Rate,
                Duration = userTrackSleep.Duration.TotalMinutes.ToString(),
                EndDate = userTrackSleep.InsertDate,
                BurnedCalories = userTrackSleep.BurnedCalories,
                _id = userTrackSleep._id
            };
            return result;
        }


        [HttpPut]
        public async Task<ApiResult<UserTrackSleepModelDTO>> PutAsync(UserTrackSleepDTO userTrackSleepDTO, CancellationToken cancellationToken)
        {
            UserTrackSleep userTrackSleep = await _UserTrackSleepRepository.GetByIdAsync(cancellationToken, userTrackSleepDTO.Id);
            System.TimeSpan duration = userTrackSleepDTO.EndDate - userTrackSleepDTO.StartDate;

            //userTrackSleep = userTrackSleepDTO.ToEntity(_mapper);
            userTrackSleep.InsertDate = userTrackSleepDTO.EndDate;
            userTrackSleep.UserId = userTrackSleepDTO.UserId;
            userTrackSleep.Rate = userTrackSleepDTO.Rate;
            userTrackSleep.Duration = duration;
            var bmr = Bmr.CalBmr(userTrackSleepDTO.weight, userTrackSleepDTO.height, userTrackSleepDTO.age, userTrackSleepDTO.gender, null);
            userTrackSleep.BurnedCalories = Convert.ToInt32((bmr / 1440) * duration.TotalMinutes);
            userTrackSleep._id = userTrackSleepDTO._id;
            userTrackSleep.Id = userTrackSleepDTO.Id;
            await _UserTrackSleepRepository.UpdateAsync(userTrackSleep, cancellationToken);


            var startdate = userTrackSleep.InsertDate - duration;
            var result = new UserTrackSleepModelDTO()
            {
                Id = userTrackSleep.Id,
                UserId = userTrackSleep.UserId,
                Rate = userTrackSleep.Rate,
                Duration = userTrackSleep.Duration.TotalMinutes.ToString(),
                EndDate = userTrackSleep.InsertDate,
                BurnedCalories = userTrackSleep.BurnedCalories,
                _id = userTrackSleep._id,
                InsertDate = startdate
            };
            return result;
        }

        [HttpDelete]
        public async Task<ApiResult> DeleteAsync(int userTrackSleepId, CancellationToken cancellationToken)
        {
            var userTrackSleep = await _UserTrackSleepRepository.GetByIdAsync(cancellationToken, userTrackSleepId);
            await _UserTrackSleepRepository.DeleteAsync(userTrackSleep, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ApiResult<UserTrackSleepSelectDTO>> GetAsync(int userId, int days, CancellationToken cancellationToken)
        {

            var listSleepDetail = new List<SleepValueAndDateModel>();
            var DurationSum = new TimeSpan(0, 0, 0);
            var userTrackSleep = await _UserTrackSleepRepository.GetByDays(userId, days).ToListAsync(cancellationToken);
            double RateSum = 0;
            double BurnedCalorieSum = 0;

            for (int d = 0; d <= days; d++)
            {
                var dateTime = DateTime.Now.AddDays(-d);
                var sleeps = userTrackSleep.Where(x => x.InsertDate.Date == dateTime.Date).ToList();
                int n = 0;
                double rate = 0;

                foreach (var item in sleeps)
                {
                    var startdate = item.InsertDate - item.Duration;
                    BurnedCalorieSum = BurnedCalorieSum + item.BurnedCalories;
                    RateSum = RateSum + item.Rate;
                    rate = rate + item.Rate;
                    DurationSum = DurationSum + item.Duration;
                    n++;
                    listSleepDetail.Add(new SleepValueAndDateModel
                    {
                        _Id = item._id,
                        Id = item.Id,
                        date = dateTime,
                        Value = sleeps.Sum(s => s.BurnedCalories),
                        startdate = startdate,
                        enddate = item.InsertDate,
                        Duration = item.Duration,
                        DailyAvragRate = Math.Round(rate / n, 2)
                    });
                }

                #region OldCode

                //var userTrackSleeps = await _UserTrackSleepRepository.GetByDays(userId, days).ToListAsync(cancellationToken);

                //var listSleepDetail = new List<SleepValueAndDateModel>();
                //var DurationSum = new TimeSpan(0, 0, 0);
                //double RateSum = 0;
                //double BurnedCalorieSum = 0;

                //for (int d = 0; d <= days; d++)
                //{
                //    var dateTime = DateTime.Now.AddDays(-d);
                //    var sleeps = userTrackSleeps.Where(s => s.InsertDate.Date == dateTime.Date);
                //    var sleepDetail = new SleepValueAndDateModel()
                //    {
                //        date = dateTime,
                //        Value = sleeps.Sum(s => s.BurnedCalories),
                //    };
                //    int n = 0;
                //    double rate = 0;
                //    foreach (var item in sleeps)
                //    {
                //        var startdate = item.InsertDate - item.Duration;
                //        sleepDetail.startdate = startdate;
                //        sleepDetail.enddate = item.InsertDate;
                //        sleepDetail.Id = item.Id;
                //        sleepDetail._Id = item._id;
                //        BurnedCalorieSum = BurnedCalorieSum + item.BurnedCalories;
                //        RateSum = RateSum + item.Rate;
                //        rate = rate + item.Rate;
                //        DurationSum = DurationSum + item.Duration;
                //        sleepDetail.Duration = sleepDetail.Duration + item.Duration;
                //        n++;
                //    }
                //    sleepDetail.DailyAvragRate = Math.Round(rate / n, 2);
                //    listSleepDetail.Add(sleepDetail);
                //};

                //var result = new UserTrackSleepSelectDTO()
                //{
                //    UserId = userId,
                //    AvrageBurnedCalories = Math.Round(BurnedCalorieSum / days, 2),
                //    AvrageRate = Math.Round(RateSum / days, 2),
                //    AvrageSleepDuration = TimeSpan.FromMinutes(DurationSum.TotalMinutes / days),
                //    SleepDetails = listSleepDetail
                //};
                //return result;

                #endregion

            };
            return new UserTrackSleepSelectDTO()
            {
                UserId = userId,
                AvrageBurnedCalories = Math.Round(BurnedCalorieSum / days, 2),
                AvrageRate = Math.Round(RateSum / days, 2),
                AvrageSleepDuration = TimeSpan.FromMinutes(DurationSum.TotalMinutes / days),
                SleepDetails = listSleepDetail
            };
        }


        [HttpGet("GetByDate")]
        public async Task<ApiResult<List<UserTrackSleepModelDTO>>> GetByDateAsync(int userId, DateTime dateTime, CancellationToken cancellationToken)
        {
            var sleepList = await _UserTrackSleepRepository.GetByDate(userId, dateTime).ToListAsync(cancellationToken);
            var result = sleepList.Select(s => new UserTrackSleepModelDTO()
            {
                Id = s.Id,
                UserId = s.UserId,
                Duration = s.Duration.TotalMinutes.ToString(),
                EndDate = s.InsertDate,
                Rate = s.Rate,
                BurnedCalories = s.BurnedCalories,
                _id = s._id
            }).ToList();
            return result;
        }


        [HttpGet("UserHistory")]
        public async Task<ApiResult<List<UserTrackSleepModelDTO>>> GetUserHistory(int userId, int days, CancellationToken cancellationToken)
        {
            var result = new List<UserTrackSleepModelDTO>();
            var sleepList = await _UserTrackSleepRepository.GetByDays(userId, days).ToListAsync(cancellationToken);
            if (sleepList != null)
            {
                result = sleepList.Select(s => new UserTrackSleepModelDTO()
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    Duration = s.Duration.TotalMinutes.ToString(),
                    EndDate = s.InsertDate,
                    Rate = s.Rate,
                    BurnedCalories = s.BurnedCalories,
                    _id = s._id
                }).ToList();
            }
            return result;
        }

    }
}
