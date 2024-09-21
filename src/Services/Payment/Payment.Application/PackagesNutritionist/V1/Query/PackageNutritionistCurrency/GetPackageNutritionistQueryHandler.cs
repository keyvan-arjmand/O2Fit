using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;
using MassTransit;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Interfaces.Services;
using Payment.Application.Dtos;

namespace Payment.Application.PackagesNutritionist.V1.Query.PackageNutritionistCurrency;

public class GetPackageNutritionistQueryHandler : IRequestHandler<GetPackageNutritionistQuery, List<PackageDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IResponseCacheService _responseCacheService;
    private readonly IMapper _mapper;
    private readonly IRequestClient<CalculateAmountO2FitPackages> _client;
    public GetPackageNutritionistQueryHandler(IResponseCacheService responseCacheService, IUnitOfWork uow, IMapper mapper, IRequestClient<CalculateAmountO2FitPackages> client)
    {
        _responseCacheService = responseCacheService;
        _uow = uow;
        _mapper = mapper;
        _client = client;
    }

    public async Task<List<PackageDto>> Handle(GetPackageNutritionistQuery request, CancellationToken cancellationToken)
    {
        CalculateAmountO2FitPackages o2FitPackageAmountO2Fit = new CalculateAmountO2FitPackages();
        List<Package> packages = new List<Package>();

        var filter = Builders<Package>.Filter.Eq(x => x.IsActive, true);
        packages = await _uow.GenericRepository<Package>().GetListOfDocumentsByFilterAsync(filter, cancellationToken);

        foreach (var package in packages.Where(x => x.PackageType == PackageType.NutritionistPack.ToDescription()).ToList())
        {
            o2FitPackageAmountO2Fit.Packages.Add(new PackagePrice
            {
                Id = package.Id,
                Price = package.Price,
            });
        }

        var discountPrice = await _client.GetResponse<CalculateAmountPackagesResult>(o2FitPackageAmountO2Fit, cancellationToken);

        List<PackageDto> selectedPackage = new List<PackageDto>();

        if (packages.Count > 0)
        {
            selectedPackage = _mapper.Map<List<Package>, List<PackageDto>>(packages);
        }
        discountPrice.Message.Packages.ForEach(x =>
        {
            selectedPackage.FirstOrDefault(y => y.Id == x.Id)!.DiscountPrice = x.DiscountPrice;
            selectedPackage.FirstOrDefault(y => y.Id == x.Id)!.Wage = x.Wage;
        });

        return selectedPackage;
    }
}