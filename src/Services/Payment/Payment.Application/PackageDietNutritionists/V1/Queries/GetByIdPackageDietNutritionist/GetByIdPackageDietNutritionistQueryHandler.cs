using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.NutritionistPackages;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Mapping;
using Payment.Application.Dtos.DietNutritionist;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;

namespace Payment.Application.PackageDietNutritionists.V1.Queries.GetByIdPackageDietNutritionist;

public class
    GetByIdPackageDietNutritionistQueryHandler : IRequestHandler<GetByIdPackageDietNutritionistQuery,
        DietNutritionistDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRequestClient<CalculateAmountNutritionistPackageById> _client;

    public GetByIdPackageDietNutritionistQueryHandler(IUnitOfWork unitOfWork,
        IRequestClient<CalculateAmountNutritionistPackageById> client)
    {
        _unitOfWork = unitOfWork;
        _client = client;
    }

    public async Task<DietNutritionistDto> Handle(GetByIdPackageDietNutritionistQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GenericRepository<DietNutritionist>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("Package not found");

        var discountPrice =
            await _client.GetResponse<CalculateAmountNutritionistPackageByIdResult>(
                new CalculateAmountNutritionistPackageById
                {
                    Id = result.Id,
                    Price = result.Price,
                    PackageCurrencyCode = result.CurrencyCode,
                    UserCurrencyCode = request.CurrencyCode
                }, cancellationToken);
        var package = result.ToDto<DietNutritionistDto>();
        package.Price = discountPrice.Message.DiscountPrice;
        package.Wage = discountPrice.Message.Wage;
        return package;
    }
}