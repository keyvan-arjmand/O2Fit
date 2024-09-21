namespace Nutritionist.Application.Dtos.NutritionistOrders;

public class NutritionistOrderDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string NutritionistId { get; set; } = string.Empty;
    public string PackageId { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public string DietCategoryName { get; set; } = string.Empty;
    public string PackageName { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}