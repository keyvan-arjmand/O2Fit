namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.ChangeNutritionistBannerAdvertiseStatus;

public class ChangeNutritionistBannerAdvertiseStatusCommandHandler : IRequestHandler<ChangeNutritionistBannerAdvertiseStatusCommand>
{
    private readonly IUnitOfWork _uow;

    public ChangeNutritionistBannerAdvertiseStatusCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ChangeNutritionistBannerAdvertiseStatusCommand request, CancellationToken cancellationToken)
    {
        var nutritionistBannerAdvertise = await _uow.GenericRepository<NutritionistBannerAdvertise>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (nutritionistBannerAdvertise == null)
            throw new NotFoundException(nameof(NutritionistBannerAdvertise), request.Id);

        nutritionistBannerAdvertise.Status = request.Status;
        await _uow.GenericRepository<NutritionistBannerAdvertise>().UpdateOneAsync(x => x.Id == request.Id,
            nutritionistBannerAdvertise, new Expression<Func<NutritionistBannerAdvertise, object>>[]
            {
                x => x.Status
            }, null, cancellationToken);
    }
}