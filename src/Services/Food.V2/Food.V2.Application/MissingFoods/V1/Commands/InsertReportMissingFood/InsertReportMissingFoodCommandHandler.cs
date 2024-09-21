using Food.V2.Domain.Aggregates.MissingFoodAggregate;

namespace Food.V2.Application.MissingFoods.V1.Commands.InsertReportMissingFood;

public class InsertReportMissingFoodCommandHandler : IRequestHandler<InsertReportMissingFoodCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IFileService _fileService;

    public InsertReportMissingFoodCommandHandler(IUnitOfWork work, IFileService fileService)
    {
        _work = work;
        _fileService = fileService;
    }

    public async Task Handle(InsertReportMissingFoodCommand request, CancellationToken cancellationToken)
    {
        var report = new MissingFood
        {
            Name = request.Name,
            Barcode = request.Barcode,
        };
        if (!string.IsNullOrWhiteSpace(request.FirstImageUri))
        {
            report.FirstImageName = _fileService.AddImage(request.FirstImageUri, "MissingFood",
                Guid.NewGuid().ToString().Substring(0, 6));
        }

        if (!string.IsNullOrWhiteSpace(request.SecondImageUri))
        {
            report.SecondImageName = _fileService.AddImage(request.SecondImageUri, "MissingFood",
                Guid.NewGuid().ToString().Substring(0, 6));
        }

        await _work.GenericRepository<MissingFood>().InsertOneAsync(report, null, cancellationToken);
    }
}