namespace Food.V2.Application.RecipeCategories.V1.Queries.IsRecipeCategoryExists;

public class IsRecipeCategoryExistsQueryHandler : IRequestHandler<IsRecipeCategoryExistsQuery, bool>
{
    private readonly IUnitOfWork _uow;

    public IsRecipeCategoryExistsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<bool> Handle(IsRecipeCategoryExistsQuery request, CancellationToken cancellationToken)
    {
        return _uow.GenericRepository<RecipeCategory>().AnyAsync(x => x.Id == request.Id, cancellationToken);
    }
}