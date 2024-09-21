using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.AppLearnCategoryAggregate;

namespace Market.Application.AppLearnCategories.V1.Commands.InsertAppLearnCategory;

public class InsertAppLearnCategoryCommandHandler:IRequestHandler<InsertAppLearnCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public InsertAppLearnCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(InsertAppLearnCategoryCommand request, CancellationToken cancellationToken)
    {
        await _work.GenericRepository<AppLearnCategory>().InsertOneAsync(new AppLearnCategory
        {
            Title = request.Title.MapTo<AppLearnCategoryTranslation, TranslationDto>()
        }, cancellationToken: cancellationToken);
    }
}