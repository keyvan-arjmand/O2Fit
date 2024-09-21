namespace Advertise.Domain.Aggregates.NutritionistBannerAdvertiseAggregate;

public class NutritionistBannerAdvertiseDescriptionTranslation : BaseEntity
{
    public NutritionistBannerAdvertiseDescriptionTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public NutritionistBannerAdvertiseDescriptionTranslation(string arabic, string english, string persian)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Arabic = arabic;
        English = english;
        Persian = persian;
    }
    public string Arabic { get; set; } 
    public string English { get; set; }
    public string Persian { get; set; }
}