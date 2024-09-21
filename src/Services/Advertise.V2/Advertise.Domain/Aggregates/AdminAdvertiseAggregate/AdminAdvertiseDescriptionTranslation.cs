namespace Advertise.Domain.Aggregates.AdminAdvertiseAggregate;

public class AdminAdvertiseDescriptionTranslation : BaseEntity
{
    public AdminAdvertiseDescriptionTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public AdminAdvertiseDescriptionTranslation(string persian, string english, string arabic)
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