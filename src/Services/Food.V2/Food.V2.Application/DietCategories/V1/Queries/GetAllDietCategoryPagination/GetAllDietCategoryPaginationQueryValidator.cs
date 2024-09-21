namespace Food.V2.Application.DietCategories.V1.Queries.GetAllDietCategoryPagination;

public class GetAllDietCategoryPaginationQueryValidator:AbstractValidator<GetAllDietCategoryPaginationQuery>
{
    public GetAllDietCategoryPaginationQueryValidator()
    {
        RuleFor(x => x.Page).NotEmpty().WithMessage("Page Can not be empty").NotNull().WithMessage("Page Can not be null");
        RuleFor(x => x.PageSize).NotEmpty().WithMessage("PageSize Can not be empty").NotNull().WithMessage("PageSize Can not be null");
    }
}