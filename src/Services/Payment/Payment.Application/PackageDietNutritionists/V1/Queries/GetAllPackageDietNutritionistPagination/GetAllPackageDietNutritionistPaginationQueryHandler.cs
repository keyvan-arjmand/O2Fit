using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;
using MassTransit;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Mapping;
using Payment.Application.Dtos.DietNutritionist;
using Payment.Domain.Aggregates.PackageDietNutritionistAggregate;

namespace Payment.Application.PackageDietNutritionists.V1.Queries.GetAllPackageDietNutritionistPagination;

public class
    GetAllPackageDietNutritionistPaginationQueryHandler : IRequestHandler<GetAllPackageDietNutritionistPaginationQuery,
        PaginationResult<DietNutritionistDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IRequestClient<CalculateAmountO2FitPackages> _client;

    public GetAllPackageDietNutritionistPaginationQueryHandler(IUnitOfWork work,
        IRequestClient<CalculateAmountO2FitPackages> client)
    {
        _work = work;
        _client = client;
    }

    public async Task<PaginationResult<DietNutritionistDto>> Handle(
        GetAllPackageDietNutritionistPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<DietNutritionist>.Filter.Eq(x => x.CurrencyCode, request.CurrencyCode);
        var result = await _work.GenericRepository<DietNutritionist>()
            .GetAllPaginationAsync(request.Page, request.PageSize, filter, cancellationToken);
        if (result.Data.Count <= 0)
        {
            return PaginationResult<DietNutritionistDto>.CreatePaginationResult(request.Page, request.PageSize,
                0, new List<DietNutritionistDto>());
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
        var packages = new List<DietNutritionistDto>();
        discountPrice.Message.Packages.ForEach(x =>
        {
            if (result.Data.Any(y => y.Id == x.Id))
            {
                var package = result.Data.FirstOrDefault(y => y.Id == x.Id).ToDto<DietNutritionistDto>();
                package.Price = x.DiscountPrice;
                package.Wage = x.Wage;
                packages.Add(package);
            }
        });
        return PaginationResult<DietNutritionistDto>.CreatePaginationResult(request.Page, request.PageSize,
            result.Data.Count, packages);
    }
}