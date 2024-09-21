namespace Identity.V2.Application.Users.V1.Queries.CheckReferralCodeAndUserIdIsValid;

public class CheckReferralCodeAndUserIdIsValidQueryValidator : AbstractValidator<CheckReferralCodeAndUserIdIsValidQuery>
{
    public CheckReferralCodeAndUserIdIsValidQueryValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code cannot be empty").NotNull()
            .WithMessage("Code cannot be null");
    }
}