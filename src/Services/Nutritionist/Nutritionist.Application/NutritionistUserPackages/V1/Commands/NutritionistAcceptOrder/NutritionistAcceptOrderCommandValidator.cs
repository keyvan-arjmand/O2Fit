namespace Nutritionist.Application.NutritionistUserPackages.V1.Commands.NutritionistAcceptOrder;

public class NutritionistAcceptOrderCommandValidator : AbstractValidator<NutritionistAcceptOrderCommand>
{
    public NutritionistAcceptOrderCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty").NotNull().WithMessage("UserId can not be null");
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId can not be empty").NotNull().WithMessage("OrderId can not be null");
        RuleFor(x => x.OrderStatus).IsInEnum();
    }
}