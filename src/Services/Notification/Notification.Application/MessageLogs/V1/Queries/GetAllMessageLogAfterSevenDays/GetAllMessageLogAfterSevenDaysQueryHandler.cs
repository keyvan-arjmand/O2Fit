namespace Notification.Application.MessageLogs.V1.Queries.GetAllMessageLogAfterSevenDays;

public class GetAllMessageLogAfterSevenDaysQueryHandler : IRequestHandler<GetAllMessageLogAfterSevenDaysQuery, List<MessageLogDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllMessageLogAfterSevenDaysQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<MessageLogDto>> Handle(GetAllMessageLogAfterSevenDaysQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<MessageLog>.Filter.Gt(x => x.Created, DateTime.UtcNow);
        filter &= Builders<MessageLog>.Filter.Eq(x => x.IsDelete, false);

        var messageLogs = await _uow.GenericRepository<MessageLog>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);

        return messageLogs.ToDto<MessageLogDto>().ToList();
    }
}