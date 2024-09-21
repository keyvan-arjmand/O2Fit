namespace Chat.Application.Messages.V1.Commands.CreateMessage;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(x => x.SenderUserId).NotEmpty().WithMessage("SenderUserId can not be empty").NotNull().WithMessage("SenderUserId can not be null");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content can not be empty").NotNull().WithMessage("Content can not be null");
        RuleFor(x => x.RecipientFullName).NotEmpty().WithMessage("RecipientUsername can not be empty").NotNull().WithMessage("RecipientUsername can not be null");
        RuleFor(x => x.SenderFullName).NotEmpty().WithMessage("SenderUsername can not be empty").NotNull().WithMessage("SenderUsername can not be null");
        RuleFor(x => x.RecipientUserId).NotEmpty().WithMessage("RecipientUserId can not be empty").NotNull().WithMessage("RecipientUserId can not be null");
        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId can not be empty").NotNull().WithMessage("GroupId can not be null");

    }
}