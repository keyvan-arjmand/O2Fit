namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseClickCountAndSubtractCostFromBudget;

public class IncreaseClickCountAndSubtractCostFromBudgetCommandValidator : AbstractValidator<IncreaseClickCountAndSubtractCostFromBudgetCommand>
{
    public IncreaseClickCountAndSubtractCostFromBudgetCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}