using Food.V2.Domain.Aggregates.FaultFoodAggregate;

namespace Food.V2.Application.ProblemFoods.V1.Commands.InsertReportMissingFood;

public class InsertReportProblemFoodCommandHandler : IRequestHandler<InsertReportProblemFoodCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IFileService _fileService;

    public InsertReportProblemFoodCommandHandler(IUnitOfWork work, IFileService fileService)
    {
        _work = work;
        _fileService = fileService;
    }

    public async Task Handle(InsertReportProblemFoodCommand request, CancellationToken cancellationToken)
    {
        if (!await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
                .AnyAsync(x => x.Id == request.FoodId, cancellationToken))
            throw new NotFoundException("Food not found");
        var report = new FaultFood
        {
            Description = request.
            FoodId = request.FoodId,
            ProblemType = request.ProblemType
        };
        await _work.GenericRepository<FaultFood>().InsertOneAsync(report, null, cancellationToken);
    }
}