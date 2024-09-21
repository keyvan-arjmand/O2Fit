namespace Food.V2.Application.Ingredients.V1.Commands.DeleteIngredientById;

public class DeleteIngredientByIdCommandHandler : IRequestHandler<DeleteIngredientByIdCommand>
{
    private readonly IUnitOfWork _uow;

    public DeleteIngredientByIdCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async  Task Handle(DeleteIngredientByIdCommand request, CancellationToken cancellationToken)
    {
        var ingredient = await _uow.GenericRepository<Ingredient>().GetByIdAsync(request.Id, cancellationToken);
        if (ingredient == null)
            throw new NotFoundException(nameof(Ingredient), request.Id);

        await _uow.GenericRepository<Ingredient>().SoftDeleteByIdAsync(request.Id, ingredient, null, cancellationToken);
    }
}