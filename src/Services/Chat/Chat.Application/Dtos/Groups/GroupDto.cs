namespace Chat.Application.Dtos.Groups;

public class GroupDto : IDto
{
    public GroupDto()
    {
        
    }

    public GroupDto(string id, string name, string userId, string userFullName, string nutritionistId, string nutritionistFullName, DateTime createdDate, List<ConnectionDto> connections)
    {
        Id = id;
        Name = name;
        UserId = userId;
        UserFullName = userFullName;
        NutritionistId = nutritionistId;
        NutritionistFullName = nutritionistFullName;
        CreatedDate = createdDate;
        Connections = connections;
    }
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string UserFullName { get; set; }
    public string NutritionistId { get; set; }
    public string NutritionistFullName { get; set; }
    public DateTime CreatedDate { get; set; }

    public List<ConnectionDto> Connections { get; set; }
}