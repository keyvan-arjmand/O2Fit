namespace Notification.Application.MessageLogs.V1.Commands.RemoveMessageLog;

public class RemoveMessageLogCommandValidator : AbstractValidator<RemoveMessageLogCommand>
{
    public RemoveMessageLogCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");

    }
}