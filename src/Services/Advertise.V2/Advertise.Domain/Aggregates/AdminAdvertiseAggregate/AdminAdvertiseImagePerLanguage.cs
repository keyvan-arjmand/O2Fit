namespace Advertise.Domain.Aggregates.AdminAdvertiseAggregate;

public class AdminAdvertiseImagePerLanguage : BaseEntity
{
    public AdminAdvertiseImagePerLanguage()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public AdminAdvertiseImagePerLanguage(string arabicImageName, string englishImageName, string persianImageName)
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