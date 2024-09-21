using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.FAQAggregate;

namespace Market.Application.FaqCategories.V1.Queries.GetAllFaqByCategoryId;

public class GetAllFaqByCategoryIdQueryHandler : IRequestHandler<GetAllFaqByCategoryIdQuery, List<FaqDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllFaqByCategoryIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<FaqDto>> Handle(GetAllFaqByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<FrequentlyAskedQuestion>.Filter.Eq(x => x.Id, request.Id);
        var result = await _work.GenericRepository<FrequentlyAskedQuestion>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        return result.ToDto<FaqDto>().ToList();
    }
}