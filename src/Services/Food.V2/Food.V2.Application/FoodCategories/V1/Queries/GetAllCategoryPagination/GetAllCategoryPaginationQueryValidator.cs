namespace Food.V2.Application.FoodCategories.V1.Queries.GetAllCategoryPagination;

public class GetAllCategoryPaginationQueryValidator:AbstractValidator<GetAllCategoryPaginationQuery>
{
    public GetAllCategoryPaginationQueryValidator()
    {
        RuleFor(x => x.Page).NotEmpty().WithMessage("Page Can not be empty").NotNull().WithMessage("Page Can not be null");
        RuleFor(x => x.PageSize).NotEmpty().WithMessage("PageSize Can not be empty").NotNull().WithMessage("PageSize Can not be null");
    }
}