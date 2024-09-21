using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Mapping;
using Discount.Application.DiscountO2Fits.V1.Queries.GetAllByUserId;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;

namespace Discount.Application.DiscountNutritionists.V1.Queries.GetAllDiscountNutritionistByUserId;

public class GetAllDiscountNutritionistByUserIdQueryHandler : IRequestHandler<GetAllDiscountByUserIdQuery, List<DiscountO2FitDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllDiscountNutritionistByUserIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<DiscountO2FitDto>> Handle(GetAllDiscountByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<DiscountO2Fit>.Filter.Eq(x => x.UserId, request.UserId.StringToObjectId());
        var result = await _work.GenericRepository<DiscountO2Fit>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return result.ToDto<DiscountO2FitDto>().ToList();
    }
}