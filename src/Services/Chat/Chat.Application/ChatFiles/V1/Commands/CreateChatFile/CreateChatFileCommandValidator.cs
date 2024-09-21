namespace Chat.Application.ChatFiles.V1.Commands.CreateChatFile;

public class CreateChatFileCommandValidator : AbstractValidator<CreateChatFileCommand>
{
    public CreateChatFileCommandValidator()
    {
        RuleFor(x => x.FileName).NotEmpty().WithMessage("FileName can not be empty").NotNull().WithMessage("FileName can not be null");
        RuleFor(x => x.FileUrl).NotEmpty().WithMessage("FileUrl can not be empty").NotNull().WithMessage("FileUrl can not be null");

    }
}