namespace Advertise.Application.Dtos.AdminAdvertises;

public class CreateAdminAdvertiseDescriptionTranslationDto
{
    public CreateAdminAdvertiseDescriptionTranslationDto()
    {
        
    }

    public CreateAdminAdvertiseDescriptionTranslationDto(string persian, string english, string arabic)
    {
        Persian = persian;
        English = english;
        Arabic = arabic;
    }
    public string Persian { get; set; }
    public string English { get; set; }
    public string Arabic { get; set; }  
}