using Market.Application.Common.Constants;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Domain.Aggregates.FAQAggregate;

namespace Market.Application.Faqs.V1.Commands.SoftDeleteFaq;

public class SoftDeleteFaqCommandHandler : IRequestHandler<SoftDeleteFaqCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public SoftDeleteFaqCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(SoftDeleteFaqCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<FrequentlyAskedQuestion>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("FAQ Not Found");
        result.IsDelete = false;
        await _work.GenericRepository<FrequentlyAskedQuestion>()
            .SoftDeleteByIdAsync(result.Id, result, null, cancellationToken);
        List<FrequentlyAskedQuestion> faqs = new List<FrequentlyAskedQuestion>();
        var redisResult = await _cacheService.GetCachedResponseAsync(RedisKey.FaqKey);
        if (!string.IsNullOrEmpty(redisResult))
        {
            faqs =
                JsonConvert.DeserializeObject<List<FrequentlyAskedQuestion>>(redisResult)!;
            faqs = faqs.Where(x => x.Id != result.Id).ToList();

            await _cacheService.ReplaceToKey(RedisKey.FaqKey, faqs, TimeSpan.FromDays(1));
        }
    }
}