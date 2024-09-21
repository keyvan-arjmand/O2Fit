namespace Food.V2.Application.DietPacks.V1.Queries.IsDietPackExitsById;

public class IsDietPackExitsByIdQueryValidator: AbstractValidator<IsDietPackExitsByIdQuery>
{
    public IsDietPackExitsByIdQueryValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull()
            .WithMessage("Id can not be null");

    }
}