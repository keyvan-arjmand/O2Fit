namespace Food.V2.Application.DietCategories.V1.Queries.GetCategoryById;

public class GetDietCategoryByIdQueryValidator:AbstractValidator<GetDietCategoryByIdQuery>
{
    public GetDietCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id can not be null")
            .NotEmpty().WithMessage("Id can not be Empty");
    }
}