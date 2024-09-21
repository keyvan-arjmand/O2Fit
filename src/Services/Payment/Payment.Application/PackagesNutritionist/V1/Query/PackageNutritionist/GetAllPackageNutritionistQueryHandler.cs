using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;
using MassTransit;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Dtos;

namespace Payment.Application.PackagesNutritionist.V1.Query.PackageNutritionist;

public class GetAllPackageNutritionistQueryHandler : IRequestHandler<GetAllPackageNutritionistQuery, List<PackageDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IRequestClient<CalculateAmountO2FitPackages> _client;
    public GetAllPackageNutritionistQueryHandler(IUnitOfWork uow, IMapper mapper, IRequestClient<CalculateAmountO2FitPackages> client)
    {
        _uow = uow;
        _mapper = mapper;
        _client = client;
    }

    public async Task<List<PackageDto>> Handle(GetAllPackageNutritionistQuery request, CancellationToken cancellationToken)
    {
        CalculateAmountO2FitPackages o2FitPackageAmountO2Fit = new CalculateAmountO2FitPackages();
        List<PackageDto> packageAdmin = new List<PackageDto>();
        var packages = new List<Package>();
        var filter = Builders<Package>.Filter.Eq(x => x.IsActive, true);
        packages = await _uow.GenericRepository<Package>().GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        if (packages.Count > 0)
        {
            foreach (var package in packages.Where(x => x.PackageType == PackageType.NutritionistPack.ToDescription()).ToList())
            {
                o2FitPackageAmountO2Fit.Packages.Add(new PackagePrice
                {
                    Id = package.Id,
                    Price = package.Price,
                });
            }
            var discountPrice = await _client.GetResponse<CalculateAmountPackagesResult>(o2FitPackageAmountO2Fit, cancellationToken);

            packageAdmin =_mapper.Map<List<Package>, List<PackageDto>>(packages).OrderByDescending(x => x.Id).ToList();
            discountPrice.Message.Packages.ForEach(x =>
            {
                packageAdmin.FirstOrDefault(y => y.Id == x.Id)!.DiscountPrice = x.DiscountPrice;
                packageAdmin.FirstOrDefault(y => y.Id == x.Id)!.Wage = x.Wage;
            });
        }

        return packageAdmin;
    }
}