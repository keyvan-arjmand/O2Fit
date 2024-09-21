using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Mapping;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.DiscountPackageNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountPackageO2FitAggregate;

namespace Discount.Application.DiscountPackagesNutritionist.V1.Query.GetByIdDiscountPackageNutritionist;

public class GetByIdDiscountNutritionistPackageQueryHandler : IRequestHandler<GetByIdDiscountNutritionistPackageQuery, DiscountPackageDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetByIdDiscountNutritionistPackageQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<DiscountPackageDto> Handle(GetByIdDiscountNutritionistPackageQuery request, CancellationToken cancellationToken)
    {
        var discount = await _uow.GenericRepository<DiscountPackageNutritionist>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (discount == null ) throw new NotFoundException("discount Not Found");
        return discount.ToDto<DiscountPackageDto>();
    }
}