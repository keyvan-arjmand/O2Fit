namespace Advertise.Application.Dtos.AdminAdvertises;

public class CreateAdminAdvertiseTitleTranslationDto
{
    public CreateAdminAdvertiseTitleTranslationDto()
    {
        
    }

    public CreateAdminAdvertiseTitleTranslationDto(string persian, string english, string arabic)
    {
        Persian = persian;
        English = english;
        Arabic = arabic;
    }
    public string Persian { get; set; }
    public string English { get; set; }
    public string Arabic { get; set; }  
}