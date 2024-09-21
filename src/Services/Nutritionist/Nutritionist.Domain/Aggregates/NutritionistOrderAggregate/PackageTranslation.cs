namespace Nutritionist.Domain.Aggregates.NutritionistOrderAggregate;

public class PackageTranslation : BaseEntity
{
    public PackageTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public PackageTranslation(string persian, string english, string arabic)
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