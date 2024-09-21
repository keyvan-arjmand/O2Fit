using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Mapping;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;

namespace Discount.Application.DiscountO2Fits.V1.Queries.GetAllByUserId;

public class GetAllDiscountByUserIdQueryHandler : IRequestHandler<GetAllDiscountByUserIdQuery, List<DiscountO2FitDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllDiscountByUserIdQueryHandler(IUnitOfWork work)
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