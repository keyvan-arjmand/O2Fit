namespace Food.V2.Application.Ingredients.V1.Commands.CreateIngredient;

public class CreateIngredientCommandValidation : AbstractValidator<CreateIngredientCommand>
{
    public CreateIngredientCommandValidation()
    {
        RuleFor(x => x.Name.Persian).NotEmpty().WithMessage("Persian name can not be empty")
            .NotNull().WithMessage("Persian name can not be null");

        RuleFor(x => x.NutrientValue).NotEmpty().WithMessage("NutrientValue can not be empty")
            .NotNull().WithMessage("NutrientValue can not be null");
        
       //RuleFor(x => x.MeasureUnitIds).NotEmpty().WithMessage("MeasureUnitIds can not be empty")
       //    .NotNull().WithMessage("MeasureUnitIds can not be null");
    }
}