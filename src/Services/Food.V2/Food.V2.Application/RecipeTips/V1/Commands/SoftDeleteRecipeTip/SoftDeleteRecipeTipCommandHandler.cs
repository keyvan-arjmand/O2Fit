using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.RecipeTips.V1.Commands.SoftDeleteRecipeTip;

public class SoftDeleteRecipeTipCommandHandler : IRequestHandler<SoftDeleteRecipeTipCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteRecipeTipCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteRecipeTipCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Recipe>.Filter.Eq(x => x.RecipeTips.Select(x => x.Id).SingleOrDefault(), request.Id);
        var result = await _work.GenericRepository<Recipe>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (result == null) throw new NotFoundException($"recipe step not found {request.Id}");
        result.RecipeTips.FirstOrDefault(x => x.Id == request.Id)!.IsDelete = true;
        await _work.GenericRepository<Recipe>().UpdateOneAsync(x => x.Id == result.Id, result,
            new Expression<Func<Recipe, object>>[]
            {
                x => x.RecipeTips
            }, null, cancellationToken);
    }
}