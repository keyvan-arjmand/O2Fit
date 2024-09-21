namespace Chat.Application.Groups.V1.Commands.CreateGroup;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.GroupName).NotEmpty().WithMessage("GroupName can not be empty").NotNull().WithMessage("GroupName can not be null");
    }
}