namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.DeleteIngredientAllergy;

public class DeleteIngredientAllergyCommandHandler : IRequestHandler<DeleteIngredientAllergyCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteIngredientAllergyCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(DeleteIngredientAllergyCommand request, CancellationToken cancellationToken)
    {
        var allergy = await _uow.GenericRepository<IngredientAllergyCategory>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (allergy == null)
            throw new NotFoundException(nameof(IngredientAllergyCategory), request.Id);
        
        allergy.IsDelete = true;
        
        await _uow.GenericRepository<IngredientAllergyCategory>()
            .SoftDeleteByIdAsync(request.Id, allergy, null, cancellationToken);
    }
}