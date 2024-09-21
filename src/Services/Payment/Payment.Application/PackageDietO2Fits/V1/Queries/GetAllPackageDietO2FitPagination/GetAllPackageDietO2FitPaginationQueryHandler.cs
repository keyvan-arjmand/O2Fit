using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;
using MassTransit;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Mapping;
using Payment.Application.Dtos.DietNutritionist;
using Payment.Application.Dtos.DietO2Fit;
using Payment.Application.PackageDietNutritionists.V1.Queries.GetAllPackageDietNutritionistPagination;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;
using Payment.Domain.Aggregates.PackageDietO2FitAggregate;

namespace Payment.Application.PackageDietO2Fits.V1.Queries.GetAllPackageDietO2FitPagination;

public class
    GetAllPackageDietO2FitPaginationQueryHandler : IRequestHandler<GetAllPackageDietO2FitPaginationQuery,
        PaginationResult<DietO2FitDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IRequestClient<CalculateAmountO2FitPackages> _client;

    public GetAllPackageDietO2FitPaginationQueryHandler(IUnitOfWork work,
        IRequestClient<CalculateAmountO2FitPackages> client)
    {
        _work = work;
        _client = client;
    }

    public async Task<PaginationResult<DietO2FitDto>> Handle(
        GetAllPackageDietO2FitPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<DietO2Fit>.Filter.Eq(x => x.CurrencyCode, request.CurrencyCode);
        var result = await _work.GenericRepository<DietO2Fit>()
            .GetAllPaginationAsync(request.Page, request.PageSize, filter, cancellationToken);
        if (result.Data.Count <= 0)
        {
            return PaginationResult<DietO2FitDto>.CreatePaginationResult(request.Page, request.PageSize,
                0, new List<DietO2FitDto>());
        }

        var discountRequest = new CalculateAmountO2FitPackages();
        discountRequest.Packages.AddRange(result.Data.Select(x => new PackagePrice
        {
            Id = x.Id,
            Price = x.Price,
            PackageCurrencyCode = x.CurrencyCode
        }));
        var discountPrice =
            await _client.GetResponse<CalculateAmountO2FitPackagesResult>(discountRequest, cancellationToken);
        var packages = new List<DietO2FitDto>();
        discountPrice.Message.Packages.ForEach(x =>
        {
            if (result.Data.Any(y => y.Id == x.Id))
            {
                var package = result.Data.FirstOrDefault(y => y.Id == x.Id).ToDto<DietO2FitDto>();
                package.Price = x.DiscountPrice;
                package.Wage = x.Wage;
                packages.Add(package);
            }
        });
        return PaginationResult<DietO2FitDto>.CreatePaginationResult(request.Page, request.PageSize,
            result.Data.Count, packages);
    }
}