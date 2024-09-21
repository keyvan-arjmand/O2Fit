using Market.Application.Common.Constants;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Application.Faqs.V1.Commands.SoftDeleteFaq;
using Market.Domain.Aggregates.FAQAggregate;
using Market.Domain.Aggregates.FAQCategoryAggregate;

namespace Market.Application.FaqCategories.V1.Commands.SoftDeleteFaqCategory;

public class SoftDeleteFaqCategoryCommandHandler : IRequestHandler<SoftDeleteFaqCategoryCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public SoftDeleteFaqCategoryCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(SoftDeleteFaqCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<FaqCategory>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("FAQ Not Found");
        result.IsDelete = false;
        await _work.GenericRepository<FaqCategory>()
            .SoftDeleteByIdAsync(result.Id, result, null, cancellationToken);
    }
}