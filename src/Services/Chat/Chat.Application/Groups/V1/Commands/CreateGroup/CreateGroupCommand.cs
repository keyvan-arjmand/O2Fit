namespace Chat.Application.Groups.V1.Commands.CreateGroup;

public record CreateGroupCommand(string GroupName, string UserId, string NutritionistId, string UserFullName, string NutritionistFullName): IRequest;