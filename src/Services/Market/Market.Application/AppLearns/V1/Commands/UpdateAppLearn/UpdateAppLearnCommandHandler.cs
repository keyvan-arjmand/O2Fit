using Market.Application.Common.Constants;
using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Interfaces.Services;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Application.Faqs.V1.Commands.UpdateFaq;
using Market.Domain.Aggregates.AppLearnAggregate;
using Market.Domain.Aggregates.AppLearnSubCategoryAggregate;
using Market.Domain.Aggregates.FAQAggregate;

namespace Market.Application.AppLearns.V1.Commands.UpdateAppLearn;

public class UpdateAppLearnCommandHandler : IRequestHandler<UpdateAppLearnCommand>
{
    private readonly IUnitOfWork _work;
    private readonly IResponseCacheService _cacheService;

    public UpdateAppLearnCommandHandler(IUnitOfWork work, IResponseCacheService cacheService)
    {
        _work = work;
        _cacheService = cacheService;
    }

    public async Task Handle(UpdateAppLearnCommand request, CancellationToken cancellationToken)
    {
        var cat = await _work.GenericRepository<AppLearnSubCategory>()
            .GetByIdAsync(request.SubCategoryId, cancellationToken);
        if (cat == null) throw new NotFoundException($"AppLearnSubCategory not found {request.Id}");
        var result = await _work.GenericRepository<AppLearn>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"AppLearn not found {request.Id}");
        result.SubCategoryId = cat.CategoryId;
        result.SubCategoryName.Arabic = cat.Title.Arabic;
        result.SubCategoryName.English = cat.Title.English;
        result.SubCategoryName.Persian = cat.Title.Persian;
        result.Question.Arabic = request.Question.Arabic;
        result.Question.English = request.Question.English;
        result.Question.Persian = request.Question.Persian;
        result.Answer = request.Answer.MapTo<List<AppLearnTranslation>, List<TranslationDto>>();
        result.VideoUrl.Arabic = request.VideoUrl.Arabic;
        result.VideoUrl.English = request.VideoUrl.English;
        result.VideoUrl.Persian = request.VideoUrl.Persian;
        if (!string.IsNullOrEmpty(request.ImageName))
        {
            result.ImageName = request.ImageName;
        }

        await _work.GenericRepository<AppLearn>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<AppLearn, object>>[]
                {
                    x => x.Answer,
                    x => x.ImageName,
                    x => x.SubCategoryId,
                    x => x.Question.Persian,
                    x => x.Question.English,
                    x => x.Question.Arabic,
                    x => x.SubCategoryName.Arabic,
                    x => x.SubCategoryName.English,
                    x => x.SubCategoryName.Persian,
                    x => x.VideoUrl.Arabic,
                    x => x.VideoUrl.English,
                    x => x.VideoUrl.Persian,
                }, null, cancellationToken);
    }
}