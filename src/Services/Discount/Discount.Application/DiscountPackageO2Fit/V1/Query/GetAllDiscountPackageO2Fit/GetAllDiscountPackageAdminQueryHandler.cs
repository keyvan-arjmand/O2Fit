using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Mapping;
using Discount.Application.Dtos;

namespace Discount.Application.DiscountPackageO2Fit.V1.Query.GetAllDiscountPackageO2Fit;

public class GetAllDiscountPackageAdminQueryHandler : IRequestHandler<GetAllDiscountPackageAdminQuery, List<DiscountPackageDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetAllDiscountPackageAdminQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<DiscountPackageDto>> Handle(GetAllDiscountPackageAdminQuery request, CancellationToken cancellationToken)
    {
        var discount = await _uow.GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>().GetAllAsync(cancellationToken);
        return discount.ToDto<DiscountPackageDto>().ToList();
    }
}