namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.DecreaseNutritionistBannerAdvertiseClickCount;

public class DecreaseNutritionistBannerAdvertiseClickCountCommandHandler : IRequestHandler<DecreaseNutritionistBannerAdvertiseClickCountCommand>
{
    private readonly IUnitOfWork _uow;

    public DecreaseNutritionistBannerAdvertiseClickCountCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DecreaseNutritionistBannerAdvertiseClickCountCommand request, CancellationToken cancellationToken)
    {
        var nutritionistAdvertiseBanner = await _uow.GenericRepository<NutritionistBannerAdvertise>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (nutritionistAdvertiseBanner == null)
            throw new NotFoundException(nameof(NutritionistBannerAdvertise), request.Id);

        nutritionistAdvertiseBanner.Budget -= new NotNegativeForDoubleTypes(nutritionistAdvertiseBanner.Cost);
        nutritionistAdvertiseBanner.ClickCount -= new NotNegativeForIntegerTypes(1);
        if (nutritionistAdvertiseBanner.Budget == 0)
        {
            nutritionistAdvertiseBanner.Status = AdvertiseStatus.OutOfBudget;
            await _uow.GenericRepository<NutritionistBannerAdvertise>().UpdateOneAsync(x => x.Id == request.Id,
                nutritionistAdvertiseBanner,
                new System.Linq.Expressions.Expression<System.Func<NutritionistBannerAdvertise, object>>[]
                {
                    a => a.ClickCount,
                    a => a.Budget,
                    a => a.Status
                }, null, cancellationToken);
        }
        else
        {
            await _uow.GenericRepository<NutritionistBannerAdvertise>().UpdateOneAsync(x => x.Id == request.Id,
                nutritionistAdvertiseBanner,
                new System.Linq.Expressions.Expression<System.Func<NutritionistBannerAdvertise, object>>[]
                {
                    a => a.ClickCount,
                    a => a.Budget
                }, null, cancellationToken);
        }
    }
}