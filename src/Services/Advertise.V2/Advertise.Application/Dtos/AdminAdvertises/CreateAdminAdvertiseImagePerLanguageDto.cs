namespace Advertise.Application.Dtos.AdminAdvertises;

public class CreateAdminAdvertiseImagePerLanguageDto
{
    public CreateAdminAdvertiseImagePerLanguageDto()
    {
        
    }

    public CreateAdminAdvertiseImagePerLanguageDto(string arabicImageUrl, string englishImageUrl, string persianImageUrl)
    {
        ArabicImageUrl = arabicImageUrl;
        EnglishImageUrl = englishImageUrl;
        PersianImageUrl = persianImageUrl;
    }
    public string ArabicImageUrl { get; set; } 
    public string EnglishImageUrl { get; set; }
    public string PersianImageUrl { get; set; }
}