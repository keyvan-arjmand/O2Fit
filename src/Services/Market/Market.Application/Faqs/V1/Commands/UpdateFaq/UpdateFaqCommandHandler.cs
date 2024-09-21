using Market.Application.Common.Constants;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.FAQAggregate;
using Market.Domain.Aggregates.FAQCategoryAggregate;

namespace Market.Application.Faqs.V1.Commands.UpdateFaq;

public class UpdateFaqCommandHandler : IRequestHandler<UpdateFaqCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public UpdateFaqCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(UpdateFaqCommand request, CancellationToken cancellationToken)
    {
        var cat = await _work.GenericRepository<FaqCategory>()
            .GetByIdAsync(request.CategoryId, cancellationToken);
        if (cat == null) throw new NotFoundException("Not Found FaqCategory");
        var result = await _work.GenericRepository<FrequentlyAskedQuestion>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"FAQ not found {request.Id}");
        result.CategoryName.Persian = cat.Title.Persian;
        result.CategoryName.English = cat.Title.English;
        result.CategoryName.Arabic = cat.Title.Arabic;
        result.CategoryId = cat.Id.StringToObjectId();
        result.Question.Persian = request.Question.Persian;
        result.Question.English = request.Question.English;
        result.Question.Arabic = request.Question.Arabic;
        result.Answer.Persian = request.Answer.Persian;
        result.Answer.English = request.Answer.English;
        result.Answer.Arabic = request.Answer.Arabic;

        await _work.GenericRepository<FrequentlyAskedQuestion>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<FrequentlyAskedQuestion, object>>[]
                {
                    x => x.CategoryName.Persian,
                    x => x.CategoryName.English,
                    x => x.CategoryName.Arabic,
                    x => x.Question.Persian,
                    x => x.Question.English,
                    x => x.Question.Arabic,
                    x => x.Answer.Persian,
                    x => x.Answer.English,
                    x => x.Answer.Arabic,
                    x => x.CategoryId,
                }, null, cancellationToken);
        List<FrequentlyAskedQuestion> faqs = new List<FrequentlyAskedQuestion>();
        var redisResult = await _cacheService.GetCachedResponseAsync(RedisKey.FaqKey);
        if (!string.IsNullOrEmpty(redisResult))
        {
            faqs =
                JsonConvert.DeserializeObject<List<FrequentlyAskedQuestion>>(redisResult)!;
            faqs = faqs.Where(x => x.Id != result.Id).ToList();
            faqs.Add(result);
            await _cacheService.ReplaceToKey(RedisKey.FaqKey, faqs, TimeSpan.FromDays(1));
        }
        else
        {
            faqs.Add(result);
            await _cacheService.CacheResponseAsync(RedisKey.FaqKey, faqs, TimeSpan.FromDays(1));
        }
    }
}