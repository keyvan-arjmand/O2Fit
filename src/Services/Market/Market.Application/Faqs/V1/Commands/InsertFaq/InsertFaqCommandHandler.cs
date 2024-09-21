using Market.Application.Common.Constants;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.FAQAggregate;
using Market.Domain.Aggregates.FAQCategoryAggregate;

namespace Market.Application.Faqs.V1.Commands.InsertFaq;

public class InsertFaqCommandHandler : IRequestHandler<InsertFaqCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public InsertFaqCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(InsertFaqCommand request, CancellationToken cancellationToken)
    {
        var cat = await _work.GenericRepository<FaqCategory>()
            .GetByIdAsync(request.CategoryId, cancellationToken);
        if (cat == null) throw new NotFoundException("Not Found FaqCategory");
        var faq = new FrequentlyAskedQuestion
        {
            Answer = request.Answer.MapTo<FaqTranslation, TranslationDto>(),
            Question = request.Question.MapTo<FaqTranslation, TranslationDto>(),
            CategoryId = cat.Id.StringToObjectId(),
            UnUseful = 0,
            Useful = 0,
            CategoryName =
            {
                Arabic = cat.Title.Arabic,
                English = cat.Title.English,
                Persian = cat.Title.Persian
            }
        };
        await _work.GenericRepository<FrequentlyAskedQuestion>().InsertOneAsync(faq, null, cancellationToken);
    }
}