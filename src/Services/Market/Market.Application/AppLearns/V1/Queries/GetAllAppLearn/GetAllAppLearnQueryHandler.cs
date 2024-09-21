using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Application.Faqs.V1.Queries.GetAllFaq;
using Market.Domain.Aggregates.FAQAggregate;

namespace Market.Application.AppLearns.V1.Queries.GetAllAppLearn;

public class GetAllAppLearnQueryHandler : IRequestHandler<GetAllFaqQuery, List<FaqDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public GetAllAppLearnQueryHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }


    public async Task<List<FaqDto>> Handle(GetAllFaqQuery request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<FrequentlyAskedQuestion>()
            .GetAllAsync(cancellationToken);
        return result.ToDto<FaqDto>().ToList();
    }
}