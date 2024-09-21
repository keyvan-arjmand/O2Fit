namespace Chat.Application.Messages.V1.Commands.ReadAllUnReadMessages;

public class ReadAllUnReadMessagesCommandValidator : AbstractValidator<ReadAllUnReadMessagesCommand>
{
    public ReadAllUnReadMessagesCommandValidator()
    {
        RuleFor(x => x.RecipientId).NotEmpty().WithMessage("RecipientId can not be empty").NotNull().WithMessage("RecipientId can not be null");
        RuleFor(x => x.CurrentUserId).NotEmpty().WithMessage("CurrentUserId can not be empty").NotNull().WithMessage("CurrentUserId can not be null");
        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId can not be empty").NotNull().WithMessage("GroupId can not be null");

    }
}