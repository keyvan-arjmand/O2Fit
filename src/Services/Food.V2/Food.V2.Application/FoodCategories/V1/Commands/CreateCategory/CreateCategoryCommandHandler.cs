using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.FoodCategories.V1.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
{
    private readonly IUnitOfWork _work;

    public CreateCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId) && !await _work.GenericRepository<Category>()
                .AnyAsync(x => x.Id == request.ParentId, cancellationToken))
            throw new NotFoundException($"Parent Not Found");
        var name = request.Translation.MapTo<CategoryTranslation, TranslationDto>();
        name.Id = ObjectId.GenerateNewId().ToString();
        var category = new Category
        {
            ParentId = string.IsNullOrWhiteSpace(request.ParentId) ? ObjectId.Empty : ObjectId.Parse(request.ParentId),
            Percent = request.Percent,
            Translation = name,
        };
        await _work.GenericRepository<Category>().InsertOneAsync(category, null, cancellationToken);
        return category.Id;
    }
}