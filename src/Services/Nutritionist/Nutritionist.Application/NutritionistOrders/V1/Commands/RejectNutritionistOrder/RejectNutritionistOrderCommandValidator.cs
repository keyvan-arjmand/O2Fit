namespace Nutritionist.Application.NutritionistOrders.V1.Commands.RejectNutritionistOrder;

public class RejectNutritionistOrderCommandValidator : AbstractValidator<RejectNutritionistOrderCommand>
{
    public RejectNutritionistOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}