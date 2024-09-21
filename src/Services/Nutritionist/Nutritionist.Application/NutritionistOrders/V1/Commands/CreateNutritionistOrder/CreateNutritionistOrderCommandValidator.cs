namespace Nutritionist.Application.NutritionistOrders.V1.Commands.CreateNutritionistOrder;

public class CreateNutritionistOrderCommandValidator : AbstractValidator<CreateNutritionistOrderCommand>
{
    public CreateNutritionistOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId can not be empty").NotNull().WithMessage("OrderId can not be null");
        RuleFor(x => x.PackageId).NotEmpty().WithMessage("PackageId can not be empty").NotNull().WithMessage("PackageId can not be null");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty").NotNull().WithMessage("UserId can not be null");
        RuleFor(x => x.NutritionistId).NotEmpty().WithMessage("NutritionistId can not be empty").NotNull().WithMessage("NutritionistId can not be null");

    }
}