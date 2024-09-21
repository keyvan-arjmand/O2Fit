namespace Food.V2.Application.Foods.V1.Queries.CheckDuplicateFoodCode;

public class CheckDuplicateFoodCodeQueryValidator : AbstractValidator<CheckDuplicateFoodCodeQuery>
{
    public CheckDuplicateFoodCodeQueryValidator()
    {
        RuleFor(x => x.FoodCode).NotEmpty().WithMessage("FoodCode can not be empty")
            .NotNull().WithMessage("FoodCode can not be null");
    }
}