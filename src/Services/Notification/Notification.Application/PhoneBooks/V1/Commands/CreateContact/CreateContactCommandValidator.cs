namespace Notification.Application.PhoneBooks.V1.Commands.CreateContact;

public class CreateContactCommandValidator: AbstractValidator<CreateContactCommand>
{
    public CreateContactCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username can not be empty").NotNull()
            .WithMessage("Username can not be null");
        RuleFor(x => x.FcmToken).NotEmpty().WithMessage("FcmToken can not be empty").NotNull()
            .WithMessage("FcmToken can not be null");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty").NotNull()
            .WithMessage("UserId can not be null");

    }
}