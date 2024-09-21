namespace Nutritionist.Domain.Aggregates.NutritionistOrderAggregate;

public class NutritionistOrder : AggregateRoot
{
    public NutritionistOrder()
    {
        
    }

    public NutritionistOrder(ObjectId userId, string username, string imageUrl, ObjectId nutritionistId, ObjectId packageId, ObjectId orderId, OrderStatus status, DietCategoryTranslation dietCategoryName, PackageTranslation packageName)
    {
        UserId = userId;
        Username = username;
        ImageUrl = imageUrl;
        NutritionistId = nutritionistId;
        PackageId = packageId;
        OrderId = orderId;
        Status = status;
        DietCategoryName = dietCategoryName;
        PackageName = packageName;
        Created = DateTime.UtcNow;
        CreatedById = userId;
        CreatedBy = username;
    }

    public ObjectId UserId { get; set; }
    public string Username { get; set; }
    public string ImageUrl { get; set; }
    public ObjectId NutritionistId { get; set; }
    public ObjectId PackageId { get; set; }
    public ObjectId OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public DietCategoryTranslation DietCategoryName { get; set; }
    public PackageTranslation PackageName { get; set; }
}