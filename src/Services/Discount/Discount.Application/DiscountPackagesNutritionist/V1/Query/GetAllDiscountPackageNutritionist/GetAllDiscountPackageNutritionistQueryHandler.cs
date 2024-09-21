using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Mapping;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.DiscountPackageNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountPackageO2FitAggregate;

namespace Discount.Application.DiscountPackagesNutritionist.V1.Query.GetAllDiscountPackageNutritionist;

public class
    GetAllDiscountPackageNutritionistQueryHandler : IRequestHandler<GetAllDiscountPackageNutritionistQuery,
        List<DiscountPackageDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetAllDiscountPackageNutritionistQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<DiscountPackageDto>> Handle(GetAllDiscountPackageNutritionistQuery request,
        CancellationToken cancellationToken)
    {
        var discount = await _uow.GenericRepository<DiscountPackageNutritionist>().GetAllAsync(cancellationToken);
        if (discount == null || discount.Count < 0) throw new NotFoundException("not exist discount ");
        return discount.ToDto<DiscountPackageDto>().ToList();
    }
}