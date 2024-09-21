namespace Chat.Application.Groups.V1.Commands.ReportGroup;

public class ReportGroupCommandHandler : IRequestHandler<ReportGroupCommand>
{
    private readonly IUnitOfWork _uow;

    public ReportGroupCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ReportGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _uow.GenericRepository<Group>().GetByIdAsync(request.GroupId, cancellationToken);
        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);

        group.ReporterUsername = request.ReporterUsername;
        group.ReporterUserId = request.ReporterUserId;
        group.IsReported = true;
        group.ReportReason = request.ReportReason;
        await _uow.GenericRepository<Group>().UpdateOneAsync(x => x.Id == request.GroupId, group,
            new Expression<Func<Group, object>>[]
            {
                x => x.ReporterUsername,
                x => x.ReporterUserId,
                x=>x.IsReported,
                x=>x.ReportReason
            }, null, cancellationToken);
    }
}