using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Mapping;
using Discount.Application.Dtos;

namespace Discount.Application.DiscountPackageO2Fit.V1.Query.GetByIdDiscountPackageO2Fit;

public class GetByIdDiscountPackageO2FitQueryHandler : IRequestHandler<GetByIdDiscountPackageO2FitQuery, DiscountPackageDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetByIdDiscountPackageO2FitQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<DiscountPackageDto> Handle(GetByIdDiscountPackageO2FitQuery request, CancellationToken cancellationToken)
    {
        var discount = await _uow.GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (discount == null) throw new NotFoundException("discount Not Found");
        return discount.ToDto<DiscountPackageDto>();
    }
}