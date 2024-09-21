namespace Chat.Application.ChatFiles.V1.Commands.DeleteChatFileById;

public class DeleteChatFileByIdCommandValidator : AbstractValidator<DeleteChatFileByIdCommand>
{
    public DeleteChatFileByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}