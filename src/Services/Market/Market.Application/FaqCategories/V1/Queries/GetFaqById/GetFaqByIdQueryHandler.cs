using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.FAQAggregate;

namespace Market.Application.FaqCategories.V1.Queries.GetFaqById;

public class GetFaqByIdQueryHandler : IRequestHandler<GetFaqByIdQuery, FaqDto>
{
    private readonly IUnitOfWork _work;

    public GetFaqByIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<FaqDto> Handle(GetFaqByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<FrequentlyAskedQuestion>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"Category FAQ not found {request.Id}");
        return result.ToDto<FaqDto>();
    }
}