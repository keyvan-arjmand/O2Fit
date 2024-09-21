namespace Food.V2.Application.Foods.V1.Queries.IsFoodExists;

public class IsFoodExistsQueryValidator : AbstractValidator<IsFoodExistsQuery>
{
    public IsFoodExistsQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}