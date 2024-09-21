namespace Nutritionist.Domain.Aggregates.NutritionistOrderAggregate;

public class DietCategoryTranslation: BaseEntity
{
    public DietCategoryTranslation()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public DietCategoryTranslation(string persian, string english, string arabic)
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