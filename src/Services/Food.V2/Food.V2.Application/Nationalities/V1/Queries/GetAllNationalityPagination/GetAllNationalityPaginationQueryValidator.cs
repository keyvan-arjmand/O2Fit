using Food.V2.Application.FoodCategories.V1.Queries.GetAllCategoryPagination;

namespace Food.V2.Application.Nationalities.V1.Queries.GetAllNationalityPagination;

public class GetAllNationalityPaginationQueryValidator:AbstractValidator<GetAllNationalityPaginationQuery>
{
    public GetAllNationalityPaginationQueryValidator()
    {
        RuleFor(x => x.Page).NotEmpty().WithMessage("Page Can not be empty").NotNull().WithMessage("Page Can not be null");
        RuleFor(x => x.PageSize).NotEmpty().WithMessage("PageSize Can not be empty").NotNull().WithMessage("PageSize Can not be null");
    }
}