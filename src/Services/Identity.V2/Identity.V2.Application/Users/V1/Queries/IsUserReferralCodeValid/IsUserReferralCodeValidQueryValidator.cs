namespace Identity.V2.Application.Users.V1.Queries.IsUserReferralCodeValid;

public class IsUserReferralCodeValidQueryValidator : AbstractValidator<IsUserReferralCodeValidQuery>
{
    public IsUserReferralCodeValidQueryValidator()
    {
        RuleFor(x => x.ReferralCode).NotEmpty().WithMessage("Referral code can not be empty")
            .NotNull().WithMessage("Referral code can not be null");
    }
}