using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.FoodCategories.V1.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public UpdateCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId) && !await _work.GenericRepository<Category>()
                .AnyAsync(x => x.Id == request.ParentId, cancellationToken))
            throw new NotFoundException($"Parent Not Found");
        var result = await _work.GenericRepository<Category>()
            .GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (result == null) throw new NotFoundException($"FoodCategory Not Found");

        result.Percent = request.Percent;
        result.Translation.Arabic = request.Translation.Arabic;
        result.Translation.English = request.Translation.English;
        result.Translation.Persian = request.Translation.Persian;
        result.ParentId = request.ParentId.StringToObjectId();

        await _work.GenericRepository<Category>()
            .UpdateOneAsync(x => x.Id == request.Id, result,
                new Expression<Func<Category, object>>[]
                {
                    x => x.Translation,
                    x => x.ParentId,
                    x => x.Percent
                }, null, cancellationToken);
    }
}