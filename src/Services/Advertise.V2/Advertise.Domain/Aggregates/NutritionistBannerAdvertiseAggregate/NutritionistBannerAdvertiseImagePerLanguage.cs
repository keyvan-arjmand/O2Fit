namespace Advertise.Domain.Aggregates.NutritionistBannerAdvertiseAggregate;

public class NutritionistBannerAdvertiseImagePerLanguage : BaseEntity
{
    public NutritionistBannerAdvertiseImagePerLanguage()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public NutritionistBannerAdvertiseImagePerLanguage(string arabicImageName, string englishImageName, string persianImageName)
    {
        Id = ObjectId.GenerateNewId().ToString();
        ArabicImageName = arabicImageName;
        EnglishImageName = englishImageName;
        PersianImageName = persianImageName;
    }
    public string ArabicImageName { get; set; } 
    public string EnglishImageName { get; set; }
    public string PersianImageName { get; set; }
}