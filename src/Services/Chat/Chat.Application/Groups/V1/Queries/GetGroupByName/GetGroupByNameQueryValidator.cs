namespace Chat.Application.Groups.V1.Queries.GetGroupByName;

public class GetGroupByNameQueryValidator : AbstractValidator<GetGroupByNameQuery>
{
    public GetGroupByNameQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty").NotNull().WithMessage("Name can not be null");
    }
}