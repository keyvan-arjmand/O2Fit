using MongoDB.Bson;
using Track.Application.Common.Exceptions;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Domain.Aggregates.TrackBurnedCalorieAggregate;
using Track.Domain.Aggregates.TrackSpecificationAggregate;

namespace Track.Application.TrackStep.V1.Commands.UpdateUserStepsByAppId;

public class UpdateUserStepsByAppIdCommandHandler : IRequestHandler<UpdateUserStepsByAppIdCommand>
{
    private readonly IUnitOfWork _work;

    public UpdateUserStepsByAppIdCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(UpdateUserStepsByAppIdCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.TrackStepAggregate.TrackStep>.Filter.Eq(x => x.AppId, request.AppId);
        var step = await _work.GenericRepository<Domain.Aggregates.TrackStepAggregate.TrackStep>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (step == null) throw new NotFoundException("step Not Found");
        var filterSpec = Builders<TrackSpecification>.Filter.Eq(x => x.CreatedById, step.UserId);
        var userSpec = await _work.TrackSpecificationRepository()
            .GetSingleLastTrackUserSpecification(filterSpec, cancellationToken);
        step.StepsCount = request.StepsCount;
        step.IsManual = request.IsManual;
        var oldTrackBurnedCalories = step.BurnedCalories;
        step.BurnedCalories = request.StepsCount.CalculateBurnedCalorieSteps(userSpec.Weight ?? 0)
            .CreateNotNegativeForDecimalType();
        await _work.GenericRepository<Domain.Aggregates.TrackStepAggregate.TrackStep>()
            .UpdateOneAsync(x => x.Id == step.Id, step,
                new Expression<Func<Domain.Aggregates.TrackStepAggregate.TrackStep, object>>[]
                {
                    x => x.StepsCount,
                    x => x.IsManual,
                    x => x.BurnedCalories,
                }, null, cancellationToken);
        //BurnedCalorieTrack
        var userId = step.UserId;
        var filterTrackBurnedCalorie =
            Builders<TrackBurnedCalorie>.Filter.Where(
                x => x.InsertDate == step.InsertDate && x.CreatedById == userId);
        var burnedWorkOutCalories = await _work.GenericRepository<TrackBurnedCalorie>()
            .GetSingleDocumentByFilterAsync(filterTrackBurnedCalorie, cancellationToken);
        burnedWorkOutCalories.Value = (burnedWorkOutCalories.Value.Value - oldTrackBurnedCalories.Value +
                                       step.BurnedCalories.Value).CreateNotNegativeForDecimalType();
        await _work.GenericRepository<TrackBurnedCalorie>()
            .UpdateOneAsync(x => x.Id == burnedWorkOutCalories.Id, burnedWorkOutCalories,
                new Expression<Func<TrackBurnedCalorie, object>>[]
                {
                    x => x.Value,
                }, null, cancellationToken);
    }
}