using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.NutritionistPackages;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Mapping;
using Payment.Application.Dtos.DietNutritionist;
using Payment.Application.Dtos.DietO2Fit;
using Payment.Application.PackageDietNutritionists.V1.Queries.GetByIdPackageDietNutritionist;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;
using Payment.Domain.Aggregates.PackageDietO2FitAggregate;

namespace Payment.Application.PackageDietO2Fits.V1.Queries.GetByIdPackageDietO2Fit;

public class
    GetByIdPackageDietO2FitQueryHandler : IRequestHandler<GetByIdPackageDietO2FitQuery,
        DietO2FitDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRequestClient<CalculateAmountNutritionistPackageById> _client;

    public GetByIdPackageDietO2FitQueryHandler(IUnitOfWork unitOfWork,
        IRequestClient<CalculateAmountNutritionistPackageById> client)
    {
        _unitOfWork = unitOfWork;
        _client = client;
    }

    public async Task<DietO2FitDto> Handle(GetByIdPackageDietO2FitQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GenericRepository<DietO2Fit>()
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
        var package = result.ToDto<DietO2FitDto>();
        package.Price = discountPrice.Message.DiscountPrice;
        package.Wage = discountPrice.Message.Wage;
        return package;
    }
}