using Common;
using Data.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.Service.v1.Command.CreatUserTrackWorkout
{
    public class CreatUserTrackWorkoutCommandHandller : IRequestHandler<CreatUserTrackWorkoutCommand, CreatUserTrackWorkoutResult>, IScopedDependency
    {
        private readonly IUserTrackWorkOutRepository _userTrackWorkOutRepository;
        private readonly IRepository<PersonalWorkOut> _personalWorkoutRepository;
        private readonly IRepository<WorkOut> _workoutRepository;
        private readonly IRepository<WorkOutAttributeValue> _workOutAttributeValueRepository;
        private readonly IBurnedWorkOutCaloriesRepository _burnedWorkOutCaloriesRepository;
        public CreatUserTrackWorkoutCommandHandller(IUserTrackWorkOutRepository userTrackWorkOutRepository,
            IRepository<WorkOutAttributeValue> workOutAttributeValueRepository,
            IBurnedWorkOutCaloriesRepository burnedWorkOutCaloriesRepository
           , IRepository<PersonalWorkOut> personalWorkoutRepository, IRepository<WorkOut> workoutRepository
            )
        {
            _userTrackWorkOutRepository = userTrackWorkOutRepository;
            _workOutAttributeValueRepository = workOutAttributeValueRepository;
            _burnedWorkOutCaloriesRepository = burnedWorkOutCaloriesRepository;
            _personalWorkoutRepository = personalWorkoutRepository;
            _personalWorkoutRepository = personalWorkoutRepository;
            _workoutRepository = workoutRepository;
        }
        public async Task<CreatUserTrackWorkoutResult> Handle(CreatUserTrackWorkoutCommand request, CancellationToken cancellationToken)
        {
            if (request.userTrackWorkOut.WorkOutId > 0)
            {

                if (request.userTrackWorkOut.WorkOutAttributeValueId > 0)
                {

                    var workoutAttrVal = await _workOutAttributeValueRepository.GetByIdAsync(cancellationToken, request.userTrackWorkOut.WorkOutAttributeValueId);
                    request.userTrackWorkOut.BurnedCalories = request.Weight * request.duration * workoutAttrVal.BurnedCalories;
                }
                else
                {

                    var workout = await _workoutRepository.GetByIdAsync(cancellationToken, request.userTrackWorkOut.WorkOutId);
                    request.userTrackWorkOut.BurnedCalories = request.Weight * request.duration * workout.BurnedCalories;
                }
            }
            else
            {
                var personalWorkout = await _personalWorkoutRepository.GetByIdAsync(cancellationToken, request.userTrackWorkOut.PersonalWorkOutId);
                request.userTrackWorkOut.BurnedCalories = request.duration * personalWorkout.Calorie / personalWorkout.Duration.TotalMinutes;
            }

            await _userTrackWorkOutRepository.AddAsync(request.userTrackWorkOut, cancellationToken);

            //-------------------------------Add in BurnedCalorieTrack----------------------
            var _burnedWorkOutCalories = await _burnedWorkOutCaloriesRepository.GetByDate(request.userTrackWorkOut.UserId, request.userTrackWorkOut.InsertDate, cancellationToken);
            var burnedWorkOutCalories = new BurnedWorkOutCalories()
            {
                InsertDate = request.userTrackWorkOut.InsertDate,
                UserId = request.userTrackWorkOut.UserId,
            };
            if (_burnedWorkOutCalories != null)
            {
                burnedWorkOutCalories._id = _burnedWorkOutCalories._id;
                burnedWorkOutCalories.Value = _burnedWorkOutCalories.Value + request.userTrackWorkOut.BurnedCalories;
                burnedWorkOutCalories.Id = _burnedWorkOutCalories.Id;
                await _burnedWorkOutCaloriesRepository.UpdateAsync(burnedWorkOutCalories, cancellationToken);
            }
            else
            {
                burnedWorkOutCalories._id = request.userTrackWorkOut._id;
                burnedWorkOutCalories.Value = request.userTrackWorkOut.BurnedCalories;
                await _burnedWorkOutCaloriesRepository.AddAsync(burnedWorkOutCalories, cancellationToken);
            }
            //--------------------------------------------------------------------------------------------------------
            CreatUserTrackWorkoutResult result = new CreatUserTrackWorkoutResult()
            {
                BurnedCalories =request.userTrackWorkOut.BurnedCalories,
                Id = request.userTrackWorkOut.Id,
                _id = request.userTrackWorkOut._id,
                PersonalWorkOutId = request.userTrackWorkOut.PersonalWorkOutId,
                UserId = request.userTrackWorkOut.UserId,
                Duration = request.userTrackWorkOut.Duration,
                InsertDate = request.userTrackWorkOut.InsertDate,
                WorkOutAttributeValueId = request.userTrackWorkOut.WorkOutAttributeValueId,
                WorkOutId = request.userTrackWorkOut.WorkOutId
            };
            return result;

        }
    }
}
