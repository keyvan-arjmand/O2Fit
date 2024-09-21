using Food.V2.Domain.Aggregates.CategoryAggregate;

namespace Food.V2.Application.FoodCategories.V1.Commands.SofDeleteCategory;

public class SofDeleteCategoryCommandHandler : IRequestHandler<SofDeleteCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public SofDeleteCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SofDeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<Category>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new AppException($"FoodCategory Not Found");
        var filter = Builders<Category>.Filter.Eq(x => x.ParentId, ObjectId.Parse(result.Id));
        var childList = await _work.GenericRepository<Category>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        
        if (childList.Count > 0)
        {
            foreach (var i in childList)
            {
                i.ParentId = ObjectId.Empty;
                await _work.GenericRepository<Category>()
                    .UpdateOneAsync(x => x.Id == i.Id, i,
                        new Expression<Func<Category, object>>[]
                        {
                            x => x.ParentId,
                        }, null, cancellationToken);
            }
        }

        result.IsDelete = true;
        await _work.GenericRepository<Category>()
            .SoftDeleteByIdAsync(result.Id, result, cancellationToken: cancellationToken);
    }
}