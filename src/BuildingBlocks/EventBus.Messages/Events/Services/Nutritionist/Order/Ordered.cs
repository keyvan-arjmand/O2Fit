namespace EventBus.Messages.Events.Services.Nutritionist.Order;

public record Ordered : BaseEvent
{
    public string UserId { get; init; }
    public string NutritionistId { get; init; }
    public string PackageId { get; init; }
    public string OrderId { get; init; }
    public string PackageNamePersian { get; init; }
    public string PackageNameEnglish { get; init; }
    public string PackageNameArabic { get; init; }  
    public string DietCategoryNamePersian { get; init; }
    public string DietCategoryNameEnglish { get; init; }
    public string DietCategoryNameArabic { get; init; }
    public string Username { get; init; }
    public string ImageProfileUserName { get; init; }
}