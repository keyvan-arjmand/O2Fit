namespace Identity.V2.Application.Countries.V1.Commands.AddCityToState;

public class AddCityToStateCommandValidator : AbstractValidator<AddCityToStateCommand>
{
    public AddCityToStateCommandValidator()
    {
        RuleFor(x => x.CountryId).NotEmpty().WithMessage("CountryId can not be empty").NotNull().WithMessage("CountryId can not be null");
        RuleFor(x => x.StateId).NotEmpty().WithMessage("StateId can not be empty").NotNull().WithMessage("StateId can not be null");
        RuleFor(x => x.Name.Arabic).NotEmpty().WithMessage("Arabic can not be empty").NotNull().WithMessage("Arabic can not be null");
        RuleFor(x => x.Name.English).NotEmpty().WithMessage("English can not be empty").NotNull().WithMessage("English can not be null");
        RuleFor(x => x.Name.Persian).NotEmpty().WithMessage("Persian can not be empty").NotNull().WithMessage("Persian can not be null");

    }
}