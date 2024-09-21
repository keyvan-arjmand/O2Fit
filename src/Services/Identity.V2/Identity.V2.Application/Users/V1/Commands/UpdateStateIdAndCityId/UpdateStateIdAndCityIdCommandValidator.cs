namespace Identity.V2.Application.Users.V1.Commands.UpdateStateIdAndCityId;

public class UpdateStateIdAndCityIdCommandValidator : AbstractValidator<UpdateStateIdAndCityIdCommand>
{
    public UpdateStateIdAndCityIdCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");
        RuleFor(x => x.StateId).NotEmpty().WithMessage("StateId can not be empty")
            .NotNull().WithMessage("StateId can not be null");
        RuleFor(x => x.CityId).NotEmpty().WithMessage("CityId can not be empty")
            .NotNull().WithMessage("CityId can not be null");
    }
}