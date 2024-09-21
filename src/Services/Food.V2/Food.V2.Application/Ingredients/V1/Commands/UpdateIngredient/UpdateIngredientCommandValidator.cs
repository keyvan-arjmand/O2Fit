namespace Food.V2.Application.Ingredients.V1.Commands.UpdateIngredient;

public class UpdateIngredientCommandValidator : AbstractValidator<UpdateIngredientCommand>
{
    public UpdateIngredientCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
        
        RuleFor(x => x.Name.Persian).NotEmpty().WithMessage("Persian name can not be empty")
            .NotNull().WithMessage("Persian name can not be null");

        RuleFor(x => x.NutrientValue).NotEmpty().WithMessage("NutrientValue can not be empty")
            .NotNull().WithMessage("NutrientValue can not be null");
        
        RuleFor(x => x.MeasureUnitIds).NotEmpty().WithMessage("MeasureUnitIds can not be empty")
            .NotNull().WithMessage("MeasureUnitIds can not be null");
    }
}