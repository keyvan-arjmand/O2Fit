namespace EventBus.Messages.Events.Services.Nutritionist;

public record NutritionistOrderCreated : BaseEvent
{
    public string NutritionistId { get; set; }
    public string PackageNamePersian { get; init; }
    public string PackageNameEnglish { get; init; }
    public string PackageNameArabic { get; init; }
}