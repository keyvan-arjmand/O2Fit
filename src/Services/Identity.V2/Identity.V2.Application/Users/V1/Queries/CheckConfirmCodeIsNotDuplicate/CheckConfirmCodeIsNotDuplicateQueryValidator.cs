namespace Identity.V2.Application.Users.V1.Queries.CheckConfirmCodeIsNotDuplicate;

public class CheckConfirmCodeIsNotDuplicateQueryValidator : AbstractValidator<CheckConfirmCodeIsNotDuplicateQuery>
{
    public CheckConfirmCodeIsNotDuplicateQueryValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code can not be empty")
            .NotNull().WithMessage("Code can not be null").MinimumLength(5)
            .WithMessage("Code min length is 5 characters");
    }
}