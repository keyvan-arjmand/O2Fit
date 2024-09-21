namespace Food.V2.Application.Brands.V1.Queries.IsBrandExits;

public class IsBrandExitsQueryValidator : AbstractValidator<IsBrandExitsQuery>
{
    public IsBrandExitsQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }
}