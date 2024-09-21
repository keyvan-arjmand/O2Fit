namespace Nutritionist.Domain.Aggregates.NutritionistUserPackageAggregate;

public class NutritionistUserPackage : AggregateRoot
{
    public NutritionistUserPackage()
    {
        
    }

    public NutritionistUserPackage(string orderId, OrderStatus orderStatus, string userId)
    {
        OrderId = ObjectId.Parse(orderId);
        OrderStatus = orderStatus;
        UserId = ObjectId.Parse(userId);
    }

    public NutritionistUserPackage(ObjectId orderId, OrderStatus orderStatus, string? dietPdfName, List<ObjectId>? dietPackIds, ObjectId userId, ObjectId nutritionistId)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        DietPdfName = dietPdfName;
        DietPackIds = dietPackIds ?? new();
        UserId = userId;
        NutritionistId = nutritionistId;
    }
    public ObjectId OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public string? DietPdfName { get; set; }
    public List<ObjectId> DietPackIds { get; set; } 
    public ObjectId UserId { get; set; }
    public ObjectId NutritionistId { get; set; }
}