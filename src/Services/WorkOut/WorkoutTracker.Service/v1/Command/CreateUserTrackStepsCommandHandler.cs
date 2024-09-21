using Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Service.v1.Command
{
    public class CreateUserTrackStepsCommandHandler : IRequestHandler<CreateUserTrackStepsCommand, UserTrackSteps>, IScopedDependency
    {
        private readonly IUserTrackStepsRepository _userTrackStepsRepository;
        private readonly IBurnedWorkOutCaloriesRepository _burnedWorkOutCaloriesRepository;

        public CreateUserTrackStepsCommandHandler(IUserTrackStepsRepository userTrackStepsRepository,
            IBurnedWorkOutCaloriesRepository burnedWorkOutCaloriesRepository)
        {
            _burnedWorkOutCaloriesRepository = burnedWorkOutCaloriesRepository;
            _userTrackStepsRepository = userTrackStepsRepository;
        }

        public async Task<UserTrackSteps> Handle(CreateUserTrackStepsCommand request, CancellationToken cancellationToken)
        {
            UserTrackSteps userTrackSteps = new UserTrackSteps
            {
                Duration = request.Duration,
                InsertDate = request.InsertDate,
                IsManual = request.IsManual,
                StepsCount = request.StepsCount,
                UserId = request.UserId,
                _id = request._id,
                BurnedCalories = request.StepsCount * 0.00061809 * request.UserWeight
            };
            userTrackSteps.Id = await _userTrackStepsRepository.AddAsync(userTrackSteps, cancellationToken);

            //-------------------------------Add in BurnedCalorieTrack----------------------
            var _burnedWorkOutCalories = await _burnedWorkOutCaloriesRepository.GetByDate(userTrackSteps.UserId,
                request.InsertDate, cancellationToken);
            var burnedWorkOutCalories = new BurnedWorkOutCalories()
            {
                InsertDate = request.InsertDate,
                UserId = request.UserId,
            };
            if (_burnedWorkOutCalories != null)
            {
                burnedWorkOutCalories.Value = _burnedWorkOutCalories.Value + userTrackSteps.BurnedCalories;
                burnedWorkOutCalories.Id = _burnedWorkOutCalories.Id;
                burnedWorkOutCalories._id = _burnedWorkOutCalories._id;
                await _burnedWorkOutCaloriesRepository.UpdateAsync(burnedWorkOutCalories, cancellationToken);
            }
            else
            {
                burnedWorkOutCalories._id = userTrackSteps._id;
                burnedWorkOutCalories.Value = userTrackSteps.BurnedCalories;
                await _burnedWorkOutCaloriesRepository.AddAsync(burnedWorkOutCalories, cancellationToken);
            }
            return userTrackSteps;
        }
    }
}
