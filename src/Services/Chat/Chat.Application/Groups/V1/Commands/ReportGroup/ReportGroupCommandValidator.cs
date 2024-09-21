namespace Chat.Application.Groups.V1.Commands.ReportGroup;

public class ReportGroupCommandValidator : AbstractValidator<ReportGroupCommand>
{
    public ReportGroupCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId can not be empty").NotNull().WithMessage("GroupId can not be null");
    }
}