namespace Food.V2.Application.DietPacks.V1.Queries.GetAllDietPackPaginated;

public class GetAllDietPackPaginatedQueryValidator : AbstractValidator<GetAllDietPackPaginatedQuery>
{
    public GetAllDietPackPaginatedQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1).WithMessage("PageIndex should greater than or equal to 1");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize should greater than or equal to 1");
        RuleFor(x => x.DietCategoryId).NotEmpty().WithMessage("DietCategoryId can not be empty")
            .NotNull().WithMessage("DietCategoryId can not be null");
    }
}