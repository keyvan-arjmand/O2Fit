using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Mapping;
using Discount.Application.DiscountO2Fits.V1.Queries.GetByIdDiscountO2Fit;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;

namespace Discount.Application.DiscountNutritionists.V1.Queries.GetByIdDiscountNutritionist;

public class GetByIdDiscountNutritionistQueryHandler : IRequestHandler<GetByIdDiscountO2FitQuery, DiscountO2FitDto>
{
    private readonly IUnitOfWork _work;

    public GetByIdDiscountNutritionistQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<DiscountO2FitDto> Handle(GetByIdDiscountO2FitQuery request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<DiscountO2Fit>().GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("discount not found");
        return result.ToDto<DiscountO2FitDto>();
    }
}