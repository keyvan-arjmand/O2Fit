namespace Advertise.Domain.Aggregates.NutritionistBannerAdvertiseAggregate;

public class NutritionistBannerAdvertiseTitleTranslation : BaseEntity
{
    public NutritionistBannerAdvertiseTitleTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public NutritionistBannerAdvertiseTitleTranslation(string arabic, string english, string persian)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Persian = persian;
        English = english;
        Arabic = arabic;
    }
    public string Persian { get; set; }
    public string English { get; set; }
    public string Arabic { get; set; }  
}