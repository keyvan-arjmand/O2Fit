namespace Nutritionist.Application.NutritionistOrders.V1.Commands.CreateNutritionistOrder;

public record CreateNutritionistOrderCommand(string UserId, string NutritionistId, string PackageId, string OrderId, string PackageNamePersian, string PackageNameEnglish, string PackageNameArabic
, string Username, string ProfileImageName, string DietCategoryPersian, string DietCategoryEnglish, string DietCategoryArabic) : IRequest;