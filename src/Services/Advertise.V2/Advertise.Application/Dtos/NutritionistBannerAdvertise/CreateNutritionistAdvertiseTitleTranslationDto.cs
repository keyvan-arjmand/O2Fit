namespace Advertise.Application.Dtos.NutritionistBannerAdvertise;

public class CreateNutritionistAdvertiseTitleTranslationDto
{
    public CreateNutritionistAdvertiseTitleTranslationDto()
    {
        
    }

    public CreateNutritionistAdvertiseTitleTranslationDto(string persian, string english, string arabic)
    {
        Persian = persian;
        English = english;
        Arabic = arabic;
    }
    public string Persian { get; set; }
    public string English { get; set; }
    public string Arabic { get; set; }  
}