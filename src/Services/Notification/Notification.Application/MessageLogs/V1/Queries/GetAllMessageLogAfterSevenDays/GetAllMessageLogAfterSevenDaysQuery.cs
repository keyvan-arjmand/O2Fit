namespace Notification.Application.MessageLogs.V1.Queries.GetAllMessageLogAfterSevenDays;

public record GetAllMessageLogAfterSevenDaysQuery() : IRequest<List<MessageLogDto>>;