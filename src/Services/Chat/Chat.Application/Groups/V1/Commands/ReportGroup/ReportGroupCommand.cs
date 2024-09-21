namespace Chat.Application.Groups.V1.Commands.ReportGroup;

public record ReportGroupCommand(string GroupId, string ReporterUsername, string ReporterUserId, ReportReason ReportReason) : IRequest;
