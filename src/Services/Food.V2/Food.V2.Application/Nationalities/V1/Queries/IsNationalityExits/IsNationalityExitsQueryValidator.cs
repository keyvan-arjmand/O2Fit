namespace Food.V2.Application.Nationalities.V1.Queries.IsNationalityExits;

public class IsNationalityExitsQueryValidator : AbstractValidator<IsNationalityExitsQuery>
{
    public IsNationalityExitsQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}