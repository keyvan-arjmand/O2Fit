using MongoDB.Bson;
using Track.Application.Common.Interfaces.Persistence.UoW;
using Track.Application.Common.Mapping;
using Track.Application.Dtos;
using Track.Domain.Aggregates.TrackBurnedCalorieAggregate;
using Track.Domain.Aggregates.TrackSpecificationAggregate;

namespace Track.Application.TrackStep.V1.Commands.InsertUserTrackSteps;

public class InsertUserTrackStepsCommandHandler : IRequestHandler<InsertUserTrackStepsCommand, UserStepsDto>
{
    private readonly IUnitOfWork _work;

    public InsertUserTrackStepsCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<UserStepsDto> Handle(InsertUserTrackStepsCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<TrackSpecification>.Filter.Eq(x => x.CreatedById, request.UserId.StringToInt());
        var userSpec = await _work.TrackSpecificationRepository()
            .GetSingleLastTrackUserSpecification(filter, cancellationToken);
        var trackSteps = new Domain.Aggregates.TrackStepAggregate.TrackStep
        {
            StepsCount = request.StepsCount,
            UserId = request.UserId.StringToInt(),
            AppId = request.AppId,
            BurnedCalories = request.StepsCount.CalculateBurnedCalorieSteps(userSpec.Weight ?? 0).CreateNotNegativeForDecimalType(),
            IsManual = request.IsManual,
            InsertDate = request.InsertDate.DateTimeFormat(),
        };
        await _work.GenericRepository<Domain.Aggregates.TrackStepAggregate.TrackStep>()
            .InsertOneAsync(trackSteps, null, cancellationToken);
        //todo BurnedCalorieTrack
        var userId = request.UserId.StringToInt();
        var filterTrackBurnedCalorie =
            Builders<TrackBurnedCalorie>.Filter.Where(
                x => x.InsertDate == request.InsertDate && x.CreatedById == userId);
        var burnedWorkOutCalories = await _work.GenericRepository<TrackBurnedCalorie>()
            .GetSingleDocumentByFilterAsync(filterTrackBurnedCalorie, cancellationToken);
        if (burnedWorkOutCalories != null)
        {
            burnedWorkOutCalories.Value = (burnedWorkOutCalories.Value.Value + trackSteps.BurnedCalories.Value).CreateNotNegativeForDecimalType();
            burnedWorkOutCalories.InsertDate = request.InsertDate;
            await _work.GenericRepository<TrackBurnedCalorie>()
                .UpdateOneAsync(x => x.Id == burnedWorkOutCalories.Id, burnedWorkOutCalories,
                    new Expression<Func<TrackBurnedCalorie, object>>[]
                    {
                        x => x.Value,
                        x => x.InsertDate,
                    }, null, cancellationToken);
        }
        else
        {
            await _work.GenericRepository<TrackBurnedCalorie>().InsertOneAsync(new TrackBurnedCalorie
            {
                AppId = trackSteps.AppId,
                Value = trackSteps.BurnedCalories,
                InsertDate = request.InsertDate,
            }, null, cancellationToken);
        }

        return trackSteps.ToDto<UserStepsDto>();
    }
}