namespace EventBus.Messages.Contracts.Services.Foods.DietCategory;

public class GetDietCategoryByIdResult
{
    public string Id { get; init; }
    public string DietCategoryNamePersian { get; init; }
    public string DietCategoryNameEnglish { get; init; }
    public string DietCategoryNameArabic { get; init; }
}