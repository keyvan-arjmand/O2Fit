using Market.Application.Common.Constants;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Application.Faqs.V1.Commands.SoftDeleteFaq;
using Market.Domain.Aggregates.AppLearnAggregate;
using Market.Domain.Aggregates.FAQAggregate;

namespace Market.Application.AppLearns.V1.Commands.SoftDeleteAppLearn;

public class SoftDeleteAppLearnCommandHandler : IRequestHandler<SoftDeleteAppLearnCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public SoftDeleteAppLearnCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(SoftDeleteAppLearnCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppLearn>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("AppLearn Not Found");
        result.IsDelete = false;
        await _work.GenericRepository<AppLearn>()
            .SoftDeleteByIdAsync(result.Id, result, null, cancellationToken);
    }
}