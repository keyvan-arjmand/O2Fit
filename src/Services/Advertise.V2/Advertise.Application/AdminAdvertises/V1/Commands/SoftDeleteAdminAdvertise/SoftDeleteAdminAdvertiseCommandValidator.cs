namespace Advertise.Application.AdminAdvertises.V1.Commands.SoftDeleteAdminAdvertise;

public class SoftDeleteAdminAdvertiseCommandValidator : AbstractValidator<SoftDeleteAdminAdvertiseCommand>
{
    public SoftDeleteAdminAdvertiseCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}