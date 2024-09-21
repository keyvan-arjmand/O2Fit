using Market.Application.Common.Constants;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Application.Faqs.V1.Commands.InsertFaq;
using Market.Domain.Aggregates.AppLearnAggregate;
using Market.Domain.Aggregates.AppLearnSubCategoryAggregate;
using Market.Domain.Aggregates.FAQAggregate;

namespace Market.Application.AppLearns.V1.Commands.InsertAppLearn;

public class InsertAppLearnCommandHandler : IRequestHandler<InsertAppLearnCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public InsertAppLearnCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(InsertAppLearnCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<AppLearnSubCategory>()
            .GetByIdAsync(request.SubCategoryId, cancellationToken);
        if (result == null) throw new NotFoundException($"AppLearnSubCategory not found {request.SubCategoryId}");
        var appLearn = new AppLearn
        {
            Answer = request.Answer.MapTo<List<AppLearnTranslation>, List<TranslationDto>>(),
            Question = request.Question.MapTo<AppLearnTranslation, TranslationDto>(),
            ImageName = request.ImageName,
            Useful = 0,
            UnUseful = 0,
            SubCategoryName = result.CategoryName.MapTo<AppLearnTranslation,AppLearnSubCategoryTranslation>(),
            SubCategoryId = result.Id.StringToObjectId(),
            VideoUrl = request.VideoUrl.MapTo<AppLearnTranslation, TranslationDto>(),
        };
        await _work.GenericRepository<AppLearn>().InsertOneAsync(appLearn, null, cancellationToken);
  
    }
}