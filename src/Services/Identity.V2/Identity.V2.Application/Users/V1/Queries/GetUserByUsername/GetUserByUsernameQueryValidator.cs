namespace Identity.V2.Application.Users.V1.Queries.GetUserByUsername;

public class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
{
    public GetUserByUsernameQueryValidator()
    {
        RuleFor(x => x.Username).NotNull().WithMessage("Username can not be null")
            .NotEmpty().WithMessage("Username can not be empty");
    }
}