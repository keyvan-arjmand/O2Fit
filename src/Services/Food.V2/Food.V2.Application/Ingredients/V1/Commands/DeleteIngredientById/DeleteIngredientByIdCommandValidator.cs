namespace Food.V2.Application.Ingredients.V1.Commands.DeleteIngredientById;

public class DeleteIngredientByIdCommandValidator : AbstractValidator<DeleteIngredientByIdCommand>
{
    public DeleteIngredientByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }
}