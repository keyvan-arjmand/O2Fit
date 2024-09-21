namespace Identity.V2.Application.Users.V1.Queries.CheckReferralCodeDuplicate;

public class CheckReferralCodeDuplicateQueryValidator:AbstractValidator<CheckReferralCodeDuplicateQuery>
{
    public CheckReferralCodeDuplicateQueryValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Referral Code can not be empty")
            .NotNull().WithMessage("Referral Code can not be null");
    }
}