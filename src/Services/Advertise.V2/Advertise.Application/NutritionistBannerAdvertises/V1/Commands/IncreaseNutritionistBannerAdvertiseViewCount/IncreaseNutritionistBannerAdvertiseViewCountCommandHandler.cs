namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.IncreaseNutritionistBannerAdvertiseViewCount;

public class IncreaseNutritionistBannerAdvertiseViewCountCommandHandler : IRequestHandler<IncreaseNutritionistBannerAdvertiseViewCountCommand>
{
    private readonly IUnitOfWork _uow;

    public IncreaseNutritionistBannerAdvertiseViewCountCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(IncreaseNutritionistBannerAdvertiseViewCountCommand request, CancellationToken cancellationToken)
    {
        var nutritionistBannerAdvertise = await _uow.GenericRepository<NutritionistBannerAdvertise>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (nutritionistBannerAdvertise == null)
            throw new NotFoundException(nameof(NutritionistBannerAdvertise), request.Id);

        nutritionistBannerAdvertise.ViewCount ++;

        await _uow.GenericRepository<NutritionistBannerAdvertise>().UpdateOneAsync(x => x.Id == request.Id,
            nutritionistBannerAdvertise, new Expression<Func<NutritionistBannerAdvertise, object>>[]
            {
                x => x.ViewCount
            }, null, cancellationToken);
    }
}