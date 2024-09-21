using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.FAQCategoryAggregate;

namespace Market.Application.FaqCategories.V1.Commands.InsertFaqCategory;

public class InsertFaqCategoryCommandHandler:IRequestHandler<InsertFaqCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public InsertFaqCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(InsertFaqCategoryCommand request, CancellationToken cancellationToken)
    {
        var cat = new FaqCategory
        {
            Title = request.Title.MapTo<FaqCategoryTranslation, TranslationDto>()
        };
        await _work.GenericRepository<FaqCategory>().InsertOneAsync(cat, null, cancellationToken);
    }
}