namespace Notification.Application.MessageLogs.V1.Commands.CreateMessageLog;

public class CreateMessageLogCommandValidator : AbstractValidator<CreateMessageLogCommand>
{
    public CreateMessageLogCommandValidator()
    {
        RuleFor(x => x.Text).NotEmpty().WithMessage("Text can not be empty").NotNull().WithMessage("Text can not be null");
        //RuleFor(x => x.ToFcmToken).NotEmpty().WithMessage("ToFcmToken can not be empty").NotNull().WithMessage("ToFcmToken can not be null");

    }
}