namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseViewAndSubtractCostFromBudget;

public class IncreaseViewCountAndSubtractCostFromBudgetCommandValidator : AbstractValidator<IncreaseViewCountAndSubtractCostFromBudgetCommand>
{
    public IncreaseViewCountAndSubtractCostFromBudgetCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}