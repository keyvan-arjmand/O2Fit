namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.DecreaseNutritionistBannerAdvertiseViewCount;

public class DecreaseNutritionistBannerAdvertiseViewCountCommandHandler : IRequestHandler<DecreaseNutritionistBannerAdvertiseViewCountCommand>
{
    private readonly IUnitOfWork _uow;

    public DecreaseNutritionistBannerAdvertiseViewCountCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DecreaseNutritionistBannerAdvertiseViewCountCommand request, CancellationToken cancellationToken)
    {
        var nutritionistBannerAdvertise = await _uow.GenericRepository<NutritionistBannerAdvertise>().GetByIdAsync(request.Id, cancellationToken);
        if (nutritionistBannerAdvertise == null)
            throw new NotFoundException(nameof(NutritionistBannerAdvertise), request.Id);
        if (nutritionistBannerAdvertise.ViewCount <= 0)
            throw new BadRequestException("View count is zero");
        
        nutritionistBannerAdvertise.ViewCount-= new NotNegativeForIntegerTypes(1);
        if (nutritionistBannerAdvertise.ViewCount <= 0)
        {
            nutritionistBannerAdvertise.Status = AdvertiseStatus.OutOfBudget;
            await _uow.GenericRepository<NutritionistBannerAdvertise>().UpdateOneAsync(x => x.Id == request.Id, nutritionistBannerAdvertise,
                new Expression<Func<NutritionistBannerAdvertise, object>>[]
                {
                    a => a.ViewCount,
                    a=>a.Status
                }, null, cancellationToken);    
        }
        else
        {
            await _uow.GenericRepository<NutritionistBannerAdvertise>().UpdateOneAsync(x => x.Id == request.Id, nutritionistBannerAdvertise,
                new Expression<Func<NutritionistBannerAdvertise, object>>[]
                {
                    a => a.ViewCount
                }, null, cancellationToken);    
        }
    }
}