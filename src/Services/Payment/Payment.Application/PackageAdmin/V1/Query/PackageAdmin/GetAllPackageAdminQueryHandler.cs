using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;
using MassTransit;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Dtos;

namespace Payment.Application.PackageAdmin.V1.Query.PackageAdmin;

public class GetAllPackageAdminQueryHandler : IRequestHandler<GetAllPackageAdminQuery, List<PackageDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IRequestClient<CalculateAmountO2FitPackages> _client;

    public GetAllPackageAdminQueryHandler(IUnitOfWork uow, IMapper mapper,
        IRequestClient<CalculateAmountO2FitPackages> client)
    {
        _uow = uow;
        _mapper = mapper;
        _client = client;
    }

    public async Task<List<PackageDto>> Handle(GetAllPackageAdminQuery request, CancellationToken cancellationToken)
    {
        CalculateAmountO2FitPackages o2FitPackageAmountO2Fit = new CalculateAmountO2FitPackages();
        List<PackageDto> packageAdmin = new List<PackageDto>();

        var filter = Builders<Package>.Filter.Eq(x => x.IsActive, true);
        List<Package> packages = await _uow.GenericRepository<Package>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);

        if (packages.Count > 0)
        {
            foreach (var package in packages)
            {
                o2FitPackageAmountO2Fit.Packages.Add(new PackagePrice
                {
                    Id = package.Id,
                    Price = package.Price,
                });
            }

            var discountPrice =
                await _client.GetResponse<CalculateAmountPackagesResult>(o2FitPackageAmountO2Fit, cancellationToken);
            packageAdmin =
                _mapper.Map<List<Package>, List<PackageDto>>(packages).OrderByDescending(x => x.Id).ToList();
            if (discountPrice.Message.Packages.Count > 0)
            {
                foreach (var i in discountPrice.Message.Packages)
                {
                    packageAdmin.FirstOrDefault(y => y.Id == i.Id)!.DiscountPrice = i.DiscountPrice;
                    packageAdmin.FirstOrDefault(y => y.Id == i.Id)!.Wage = i.Wage;
                }
            }
        }
        return packageAdmin;
    }
}