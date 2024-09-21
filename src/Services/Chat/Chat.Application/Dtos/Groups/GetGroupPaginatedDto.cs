namespace Chat.Application.Dtos.Groups;

public class GetGroupPaginatedDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string UserFullName { get; set; }
    public string NutritionistId { get; set; }
    public string NutritionistFullName { get; set; }
    public string ReporterUsername { get; set; }
    public string ReporterUserId { get; set; }
    public bool IsReported { get; set; }
    public ReportReason ReportReason { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<MessageDto> Messages { get; set; }
}