namespace Advertise.Application.AdminAdvertises.V1.Commands.DecreaseViewCount;

public class DecreaseViewCountCommandValidator : AbstractValidator<DecreaseViewCountCommand>
{
    public DecreaseViewCountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}