namespace Advertise.Domain.Aggregates.NutritionistBannerAdvertiseAggregate;

public class NutritionistBannerAdvertise : AggregateRoot
{
    public NutritionistBannerAdvertise()
    {
        
    }

    public NutritionistBannerAdvertise(string link, double cost, NotNegativeForDoubleTypes budget, NutritionistBannerAdvertiseImagePerLanguage image, NutritionistBannerAdvertiseTitleTranslation title, NutritionistBannerAdvertiseDescriptionTranslation description, AdvertiseStatus status, List<Language> languages, int mainClickCount, int viewCount, NotNegativeForIntegerTypes clickCount, List<ObjectId> statesIds)
    {
        Link = link;
        Cost = cost;
        Budget = budget;
        Image = image;
        Title = title;
        Description = description;
        Status = status;
        Languages = languages;
        MainClickCount = mainClickCount;
        ViewCount = viewCount;
        ClickCount = clickCount;
        StatesIds = statesIds;
    }
    public string Link { get; set; }
    public double Cost { get; set; }
    public NotNegativeForDoubleTypes Budget { get; set; } 
    public NutritionistBannerAdvertiseImagePerLanguage Image { get; set; } 
    public NutritionistBannerAdvertiseTitleTranslation Title { get; set; } 
    public NutritionistBannerAdvertiseDescriptionTranslation Description { get; set; } 
    public AdvertiseStatus Status { get; set; }
    public List<Language> Languages { get; set; }
    public int MainClickCount { get; set; }
    public int ViewCount { get; set; }
    public NotNegativeForIntegerTypes ClickCount { get; set; }
    public List<ObjectId> StatesIds { get; set; }
}