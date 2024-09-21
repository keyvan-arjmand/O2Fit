namespace Identity.V2.Application.Users.V1.Commands.IncreaseReferralCountBuy;

public class IncreaseReferralCountBuyCommandValidator : AbstractValidator<IncreaseReferralCountBuyCommand>
{
    public IncreaseReferralCountBuyCommandValidator()
    {
        RuleFor(x => x.ReferralInviter).NotEmpty().WithMessage("ReferralInviter can not be empty")
            .NotNull().WithMessage("ReferralInviter can not be null");
    }
}