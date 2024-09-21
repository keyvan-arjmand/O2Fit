using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Domain.Aggregates.TrackBurnedCalorieAggregate;
using ZstdSharp.Unsafe;

namespace Track.Application.TrackStep.V1.Commands.SoftDeleteUserSteps;

public class SoftDeleteUserStepsCommandHandler : IRequestHandler<SoftDeleteUserStepsCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteUserStepsCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteUserStepsCommand request, CancellationToken cancellationToken)
    {
        var userStep = await _work.GenericRepository<Domain.Aggregates.TrackStepAggregate.TrackStep>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (userStep == null) throw new NotFoundException($"step Not Found {request.Id}");
        userStep!.IsDelete = true;
        await _work.GenericRepository<Domain.Aggregates.TrackStepAggregate.TrackStep>()
            .SoftDeleteByIdAsync(request.Id, userStep, null, cancellationToken);
        //burned Calorie
        var userId = userStep.UserId;
        var filterTrackBurnedCalorie =
            Builders<TrackBurnedCalorie>.Filter.Where(
                x => x.InsertDate == userStep.InsertDate && x.CreatedById == userId);
        var burnedWorkOutCalories = await _work.GenericRepository<TrackBurnedCalorie>()
            .GetSingleDocumentByFilterAsync(filterTrackBurnedCalorie, cancellationToken);
        burnedWorkOutCalories.Value = (burnedWorkOutCalories.Value.Value - userStep.BurnedCalories.Value).CreateNotNegativeForDecimalType();
        await _work.GenericRepository<TrackBurnedCalorie>()
            .UpdateOneAsync(x => x.Id == burnedWorkOutCalories.Id, burnedWorkOutCalories,
                new Expression<Func<TrackBurnedCalorie, object>>[]
                {
                    x => x.Value,
                }, null, cancellationToken);
    }
}