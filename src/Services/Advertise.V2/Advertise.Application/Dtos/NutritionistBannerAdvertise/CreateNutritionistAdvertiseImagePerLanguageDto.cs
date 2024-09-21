namespace Advertise.Application.Dtos.NutritionistBannerAdvertise;

public class CreateNutritionistAdvertiseImagePerLanguageDto
{
    public CreateNutritionistAdvertiseImagePerLanguageDto()
    {
        
    }

    public CreateNutritionistAdvertiseImagePerLanguageDto(string arabicImageUrl, string englishImageUrl, string persianImageUrl)
    {
        ArabicImageUrl = arabicImageUrl;
        EnglishImageUrl = englishImageUrl;
        PersianImageUrl = persianImageUrl;
    }
    public string ArabicImageUrl { get; set; } 
    public string EnglishImageUrl { get; set; }
    public string PersianImageUrl { get; set; }
}