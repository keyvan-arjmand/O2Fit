using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;
using MassTransit;
using Payment.Application.Common.Exceptions;
using Payment.Application.Common.Interfaces.Persistence.UoW;
using Payment.Application.Common.Mapping;
using Payment.Application.Dtos;

namespace Payment.Application.PackageAdmin.V1.Query.PackagePaginationAdmin;

public class
    GetAllPackageAdminPaginationQueryHandler : IRequestHandler<GetAllPackageAdminPaginationQuery, PaginationResult<PackageDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IRequestClient<CalculateAmountO2FitPackages> _client;

    public GetAllPackageAdminPaginationQueryHandler(IUnitOfWork uow, IMapper mapper,
        IRequestClient<CalculateAmountO2FitPackages> client)
    {
        _uow = uow;
        _mapper = mapper;
        _client = client;
    }

    public async Task<PaginationResult<PackageDto>> Handle(GetAllPackageAdminPaginationQuery request,
        CancellationToken cancellationToken)
    {
        CalculateAmountO2FitPackages o2FitPackageAmountO2Fit = new CalculateAmountO2FitPackages();
        PaginationResult<Package> packages = await _uow.GenericRepository<Package>()
            .GetAllPaginationAsync(request.Page, request.PageSize, cancellationToken);
        if (packages.Count < 0 || packages == null) throw new NotFoundException("not exist package");

        foreach (var package in packages.Data.Where(x => x.IsActive))
        {
            o2FitPackageAmountO2Fit.Packages.Add(new PackagePrice
            {
                Id = package.Id,
                Price = package.Price,
            });
        }

        var discountPrice =
            await _client.GetResponse<CalculateAmountPackagesResult>(o2FitPackageAmountO2Fit, cancellationToken);
        var pagingPackage = PaginationResult<PackageDto>.CreatePaginationResult(request.Page, request.PageSize,
            packages.Data.Count, packages.Data.Where(x => x.IsActive).ToDto<PackageDto>().ToList());

        discountPrice.Message.Packages.ForEach(x =>
        {
            pagingPackage.Data.FirstOrDefault(y => y.Id == x.Id)!.DiscountPrice = x.DiscountPrice;
            pagingPackage.Data.FirstOrDefault(y => y.Id == x.Id)!.Wage = x.Wage;
        });

        return pagingPackage;
    }
}