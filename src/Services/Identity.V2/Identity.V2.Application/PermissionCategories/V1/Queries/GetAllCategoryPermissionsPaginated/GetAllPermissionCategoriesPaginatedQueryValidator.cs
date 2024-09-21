namespace Identity.V2.Application.PermissionCategories.V1.Queries.GetAllCategoryPermissionsPaginated;

public class GetAllPermissionCategoriesPaginatedQueryValidator : AbstractValidator<GetAllPermissionCategoriesPaginatedQuery>
{
    public GetAllPermissionCategoriesPaginatedQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0).WithMessage("PageIndex should equal or greater than zero");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(0).WithMessage("PageSize should equal or greater than zero");
    }
}