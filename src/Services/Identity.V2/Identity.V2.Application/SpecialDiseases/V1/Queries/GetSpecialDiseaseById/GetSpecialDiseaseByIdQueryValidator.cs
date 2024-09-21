namespace Identity.V2.Application.SpecialDiseases.V1.Queries.GetSpecialDiseaseById;

public class GetSpecialDiseaseByIdQueryValidator : AbstractValidator<GetSpecialDiseaseByIdQuery>
{
    public GetSpecialDiseaseByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }
}