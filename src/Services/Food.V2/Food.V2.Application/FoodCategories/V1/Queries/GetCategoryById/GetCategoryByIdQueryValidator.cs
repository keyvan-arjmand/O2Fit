namespace Food.V2.Application.FoodCategories.V1.Queries.GetCategoryById;

public class GetCategoryByIdQueryValidator:AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id can not be null")
            .NotEmpty().WithMessage("Id can not be Empty");
    }
}