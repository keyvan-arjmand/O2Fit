namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseViewCount;

public class IncreaseViewCountCommandValidator : AbstractValidator<IncreaseViewCountCommand>
{
    public IncreaseViewCountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}