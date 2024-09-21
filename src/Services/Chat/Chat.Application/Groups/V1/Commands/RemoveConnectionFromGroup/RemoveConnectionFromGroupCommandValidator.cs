namespace Chat.Application.Groups.V1.Commands.RemoveConnectionFromGroup;

public class RemoveConnectionFromGroupCommandValidator: AbstractValidator<RemoveConnectionFromGroupCommand>
{
    public RemoveConnectionFromGroupCommandValidator()
    {
        RuleFor(x => x.ConnectionId).NotEmpty().WithMessage("ConnectionId can not be empty").NotNull().WithMessage("ConnectionId can not be null");
        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId can not be empty").NotNull().WithMessage("GroupId can not be null");

    }
}