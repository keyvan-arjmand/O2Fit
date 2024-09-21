namespace Identity.V2.Domain.Aggregates.UserAggregate;

public class NutritionistList : BaseEntity
{
    public ObjectId DietCategoryId { get; set; }
    public ObjectId NutritionistId { get; set; }
    //?? public ObjectId PackageId { get; set; }
    public DateTime NutritionistPkExpireDate { get; set; }
}