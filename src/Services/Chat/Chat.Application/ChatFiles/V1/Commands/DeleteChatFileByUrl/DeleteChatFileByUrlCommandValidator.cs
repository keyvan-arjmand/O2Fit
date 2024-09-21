namespace Chat.Application.ChatFiles.V1.Commands.DeleteChatFileByUrl;

public class DeleteChatFileByUrlCommandValidator : AbstractValidator<DeleteChatFileByUrlCommand>
{
    public DeleteChatFileByUrlCommandValidator()
    {
        RuleFor(x => x.Url).NotEmpty().WithMessage("Url can not be empty").NotNull().WithMessage("Url can not be null");

    }
}