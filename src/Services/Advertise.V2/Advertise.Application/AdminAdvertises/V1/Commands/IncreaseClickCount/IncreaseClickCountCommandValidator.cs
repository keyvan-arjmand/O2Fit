namespace Advertise.Application.AdminAdvertises.V1.Commands.IncreaseClickCount;

public class IncreaseClickCountCommandValidator : AbstractValidator<IncreaseClickCountCommand>
{
    public IncreaseClickCountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}