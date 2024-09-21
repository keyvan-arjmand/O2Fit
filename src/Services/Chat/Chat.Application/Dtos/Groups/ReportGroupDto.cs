namespace Chat.Application.Dtos.Groups;

public class ReportGroupDto : IDto
{
    public string Id { get; set; }
    public ReportReason ReportReason { get; set; }
}