namespace Identity.V2.Application.Users.V1.Queries.GetUserInfoById;

public class GetUserInfoByIdQueryValidator: AbstractValidator<GetUserInfoByIdQuery>
{
    public GetUserInfoByIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty")
            .NotNull().WithMessage("UserId can not be null");
    }
}