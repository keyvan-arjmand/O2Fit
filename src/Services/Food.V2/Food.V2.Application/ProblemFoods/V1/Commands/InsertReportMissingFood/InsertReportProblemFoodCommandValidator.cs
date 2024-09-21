namespace Food.V2.Application.ProblemFoods.V1.Commands.InsertReportMissingFood;

public class InsertReportProblemFoodCommandValidator : AbstractValidator<InsertReportProblemFoodCommand>
{
    public InsertReportProblemFoodCommandValidator()
    {
        RuleFor(x => x.FoodId).NotNull().NotEmpty().WithMessage("FoodId cannot null or empty");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot null or empty");
        RuleFor(x => x.ProblemType).IsInEnum();
    }
}