namespace Chat.Application.Groups.V1.Commands.CreateGroupWithConnection;

public class CreateGroupWithConnectionCommandValidator : AbstractValidator<CreateGroupWithConnectionCommand>
{
    public CreateGroupWithConnectionCommandValidator()
    {
        RuleFor(x => x.GroupName).NotEmpty().WithMessage("GroupName can not be empty").NotNull().WithMessage("GroupName can not be null");
    }
}