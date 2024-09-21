namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.RemoveChildFromIngredientAllergyCategory;

public class
    RemoveChildFromIngredientAllergyCategoryCommandHandler : IRequestHandler<
        RemoveChildFromIngredientAllergyCategoryCommand>
{
    private readonly IUnitOfWork _uow;

    public RemoveChildFromIngredientAllergyCategoryCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(RemoveChildFromIngredientAllergyCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var allergy = await _uow.GenericRepository<IngredientAllergyCategory>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (allergy == null)
            throw new NotFoundException(nameof(IngredientAllergyCategory), request.Id);


        var filter = Builders<IngredientAllergyCategory>.Filter.Eq(x => x.Id, request.Id);
        var filter2 =
            Builders<IngredientAllergyCategory>.Filter.AnyEq(x => x.IngredientAllergiesIds, ObjectId.Parse(request.ChildId));
        var filter3 = Builders<IngredientAllergyCategory>.Filter.Eq(x => x.IsDelete, false);

        var getAllergy = await _uow.GenericRepository<IngredientAllergyCategory>()
            .GetSingleDocumentByFilterAsync(filter & filter2 & filter3, cancellationToken);

        if (getAllergy != null)
        {
            var update =
                Builders<IngredientAllergyCategory>.Update.Pull(x => x.IngredientAllergiesIds,
                    ObjectId.Parse(request.ChildId));
            await _uow.GenericRepository<IngredientAllergyCategory>()
                .UpdateOneAsync(filter, allergy, update, null, cancellationToken);
        }
    }
}