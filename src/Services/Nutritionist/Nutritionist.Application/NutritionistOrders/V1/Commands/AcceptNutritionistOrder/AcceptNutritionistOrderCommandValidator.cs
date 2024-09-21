namespace Nutritionist.Application.NutritionistOrders.V1.Commands.AcceptNutritionistOrder;

public class AcceptNutritionistOrderCommandValidator : AbstractValidator<AcceptNutritionistOrderCommand>
{
    public AcceptNutritionistOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}