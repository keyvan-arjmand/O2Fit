namespace Identity.V2.Application.Countries.V1.Commands.SoftDeleteByOldSystemId;

public class SoftDeleteByOldSystemIdCommandValidator: AbstractValidator<SoftDeleteByOldSystemIdCommand>
{
    public SoftDeleteByOldSystemIdCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("Id can not be null")
            .GreaterThan(0).WithMessage("Id must greater than 0");

    }
}