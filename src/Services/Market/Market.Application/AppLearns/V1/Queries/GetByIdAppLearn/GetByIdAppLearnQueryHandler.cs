using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Application.Faqs.V1.Queries.GetByIdFaq;
using Market.Domain.Aggregates.FAQAggregate;

namespace Market.Application.AppLearns.V1.Queries.GetByIdAppLearn;

public class GetByIdAppLearnQueryHandler : IRequestHandler<GetByIdFaqQuery, FaqDto>
{
    private readonly IUnitOfWork _work;

    public GetByIdAppLearnQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<FaqDto> Handle(GetByIdFaqQuery request, CancellationToken cancellationToken)
    {
        var faq = await _work.GenericRepository<FrequentlyAskedQuestion>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (faq == null) throw new NotFoundException("Not Found faq");
        return faq.ToDto<FaqDto>();
    }
}