namespace Chat.Application.Groups.V1.Commands.AddConnectionToGroup;

public class AddConnectionToGroupCommandValidator : AbstractValidator<AddConnectionToGroupCommand>
{
    public AddConnectionToGroupCommandValidator()
    {
        RuleFor(x => x.GroupName).NotEmpty().WithMessage("GroupName can not be empty").NotNull().WithMessage("GroupName can not be null");
    }
}