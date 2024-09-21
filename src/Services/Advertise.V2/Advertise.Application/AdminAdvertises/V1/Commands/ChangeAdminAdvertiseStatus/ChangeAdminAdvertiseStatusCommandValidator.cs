namespace Advertise.Application.AdminAdvertises.V1.Commands.ChangeAdminAdvertiseStatus;

public class ChangeAdminAdvertiseStatusCommandValidator : AbstractValidator<ChangeAdminAdvertiseStatusCommand>
{
    public ChangeAdminAdvertiseStatusCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
        RuleFor(x => x.Status).IsInEnum();
    }
}