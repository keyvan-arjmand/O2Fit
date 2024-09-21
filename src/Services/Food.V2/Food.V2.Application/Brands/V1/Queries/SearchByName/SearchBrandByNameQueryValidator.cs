namespace Food.V2.Application.Brands.V1.Queries.SearchByName;

public class SearchBrandByNameQueryValidator : AbstractValidator<SearchBrandByNameQuery>
{
    public SearchBrandByNameQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name NotNull NotEmpty");
    }
}