namespace Food.V2.Application.IngredientAllergyCategories.V1.Commands.DeleteIngredientAllergy;

public class DeleteIngredientAllergyValidator : AbstractValidator<DeleteIngredientAllergyCommand>
{
    public DeleteIngredientAllergyValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }    
}