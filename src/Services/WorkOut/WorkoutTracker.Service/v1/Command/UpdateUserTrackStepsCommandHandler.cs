using Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.Service.v1.Command
{
    public class UpdateUserTrackStepsCommandHandler : IRequestHandler<UpdateUserTrackStepsCommand, Unit>, IScopedDependency
    {
        private readonly IUserTrackStepsRepository _userTrackStepsRepository;
        private readonly IBurnedWorkOutCaloriesRepository _burnedWorkOutCaloriesRepository;

        public UpdateUserTrackStepsCommandHandler(IUserTrackStepsRepository userTrackStepsRepository,
            IBurnedWorkOutCaloriesRepository burnedWorkOutCaloriesRepository)
        {
            _userTrackStepsRepository = userTrackStepsRepository;
            _burnedWorkOutCaloriesRepository = burnedWorkOutCaloriesRepository;
        }

        public async Task<Unit> Handle(UpdateUserTrackStepsCommand request, CancellationToken cancellationToken)
        {
            UserTrackSteps OlduserTrackSteps = await _userTrackStepsRepository.GetByIdAsync(cancellationToken, request.Id);
            _userTrackStepsRepository.Detach(OlduserTrackSteps);

            UserTrackSteps userTrackSteps = new UserTrackSteps
            {
                Duration = request.Duration,
                InsertDate = request.InsertDate,
                Id = request.Id,
                IsManual = request.IsManual,
                UserId = request.UserId,
                StepsCount = request.StepsCount,
                BurnedCalories = request.StepsCount * 0.00061809 * request.UserWeight,
                _id = OlduserTrackSteps._id
            };
            await _userTrackStepsRepository.UpdateAsync(userTrackSteps, cancellationToken);
            //-------------------------------update in BurnedCalorieTrack----------------------
            var _burnedWorkOutCalories = await _burnedWorkOutCaloriesRepository.GetByDate(userTrackSteps.UserId, request.InsertDate, cancellationToken);
            var burnedWorkOutCalories = new BurnedWorkOutCalories()
            {
                InsertDate = userTrackSteps.InsertDate,
                UserId = userTrackSteps.UserId,
                Id = _burnedWorkOutCalories.Id,
                _id = _burnedWorkOutCalories._id,
                Value = _burnedWorkOutCalories.Value - OlduserTrackSteps.BurnedCalories + userTrackSteps.BurnedCalories
            };
            await _burnedWorkOutCaloriesRepository.UpdateAsync(burnedWorkOutCalories, cancellationToken);

            return Unit.Value;
        }
    }
}
