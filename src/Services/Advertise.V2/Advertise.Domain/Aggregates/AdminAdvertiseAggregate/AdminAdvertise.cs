namespace Advertise.Domain.Aggregates.AdminAdvertiseAggregate;

public class AdminAdvertise : AggregateRoot
{
    public AdminAdvertise()
    {
        
    }
    public AdminAdvertise(string link, int mainClickCount, double cost, NotNegativeForDoubleTypes budget, AdminAdvertiseImagePerLanguage image, AdminAdvertiseTitleTranslation title, AdminAdvertiseDescriptionTranslation description, AdvertiseStatus status, List<Language> languages, NotNegativeForIntegerTypes viewCount, NotNegativeForIntegerTypes clickCount, List<ObjectId> statesIds)
    {
        Link = link;
        MainClickCount = mainClickCount;
        Cost = cost;
        Budget = budget;
        Image = image;
        Title = title;
        Description = description;
        Status = status;
        Languages = languages;
        ViewCount = viewCount;
        ClickCount = clickCount;
        StatesIds = statesIds;
    }
    public string Link { get; set; }
    public int MainClickCount { get; set; }
    public double Cost { get; set; }
    public NotNegativeForDoubleTypes Budget { get; set; } 
    public AdminAdvertiseImagePerLanguage Image { get; set; } 
    public AdminAdvertiseTitleTranslation Title { get; set; } 
    public AdminAdvertiseDescriptionTranslation Description { get; set; } 
    public AdvertiseStatus Status { get; set; }
    public List<Language> Languages { get; set; }
    public NotNegativeForIntegerTypes ViewCount { get; set; }
    public NotNegativeForIntegerTypes ClickCount { get; set; }
    public List<ObjectId> StatesIds { get; set; }
}