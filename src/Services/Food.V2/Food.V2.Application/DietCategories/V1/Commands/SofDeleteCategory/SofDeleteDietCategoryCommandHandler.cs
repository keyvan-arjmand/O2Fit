using Food.V2.Domain.Aggregates.CategoryAggregate;
using Food.V2.Domain.Aggregates.DietCategoryAggregate;

namespace Food.V2.Application.DietCategories.V1.Commands.SofDeleteCategory;

public class SofDeleteDietCategoryCommandHandler : IRequestHandler<SofDeleteDietCategoryCommand>
{
    private readonly IUnitOfWork _work;

    public SofDeleteDietCategoryCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SofDeleteDietCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<DietCategory>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (request == null) throw new NotFoundException("DietCategory Not Found");
        var filter = Builders<DietCategory>.Filter.Eq(x => x.ParentId, ObjectId.Parse(result.Id));
        var childList = await _work.GenericRepository<DietCategory>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        if (childList.Count > 0)
        {
            foreach (var i in childList)
            {
                i.ParentId = ObjectId.Empty;
                await _work.GenericRepository<DietCategory>()
                    .UpdateOneAsync(x => x.Id == i.Id, i,
                        new Expression<Func<DietCategory, object>>[]
                        {
                            x => x.ParentId,
                        }, null, cancellationToken);
            }
        }

        result.IsDelete = true;
        await _work.GenericRepository<DietCategory>()
            .SoftDeleteByIdAsync(result.Id, result, cancellationToken: cancellationToken);
    }
}