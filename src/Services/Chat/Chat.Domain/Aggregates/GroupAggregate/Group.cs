namespace Chat.Domain.Aggregates.GroupAggregate;

public class Group : AggregateRoot
{
    public Group()
    {
        
    }

    public Group(string name)
    {
        Name = name;
    }

    public Group(string name, string userId, string userFullName, string nutritionistId, string nutritionistFullName)
    {
        Name = name;
        UserId = userId;
        UserFullName = userFullName;
        NutritionistId = nutritionistId;
        NutritionistFullName = nutritionistFullName;
    }

    public Group(string name, List<Connection> connections)
    {
        Name = name;
        Connections.AddRange(connections);
    }

    public Group(string name, string userId, string userFullName, string nutritionistId, string nutritionistFullName, string reporterUsername, string reporterUserId, bool isReported)
    {
        Name = name;
        UserId = userId;
        UserFullName = userFullName;
        NutritionistId = nutritionistId;
        NutritionistFullName = nutritionistFullName;
        ReporterUsername = reporterUsername;
        ReporterUserId = reporterUserId;
        IsReported = isReported;
    }

    public Group(string name, string userId, string userFullName, string nutritionistId, string nutritionistFullName, string reporterUsername, string reporterUserId, ReportReason reportReason, bool isReported)
    {
        Name = name;
        UserId = userId;
        UserFullName = userFullName;
        NutritionistId = nutritionistId;
        NutritionistFullName = nutritionistFullName;
        ReporterUsername = reporterUsername;
        ReporterUserId = reporterUserId;
        ReportReason = reportReason;
        IsReported = isReported;
    }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string UserFullName { get; set; }
    public string NutritionistId { get; set; }
    public string NutritionistFullName { get; set; }
    public string ReporterUsername { get; set; }
    public string ReporterUserId { get; set; }
    public ReportReason ReportReason { get; set; }
    public bool IsReported { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public List<Connection> Connections { get; set; } = new();
    public List<Message> Messages { get; set; } = new();
}