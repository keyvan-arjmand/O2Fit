namespace Notification.Application.PhoneBooks.V1.Queries.GetUserDataFromPhoneBook;

public class GetUserDataFromPhoneBookQueryValidator : AbstractValidator<GetUserDataFromPhoneBookQuery>
{
    public GetUserDataFromPhoneBookQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty").NotNull().WithMessage("UserId can not be null");
    }
}