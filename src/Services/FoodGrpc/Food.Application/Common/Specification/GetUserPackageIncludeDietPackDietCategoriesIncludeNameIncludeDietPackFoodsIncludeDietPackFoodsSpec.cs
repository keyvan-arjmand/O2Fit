using Food.Domain.Entities;
using Food.Domain.Enum;
using StackExchange.Redis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Food.Application.Common.Specification;

public class GetUserPackageIncludeDietPackDietCategoriesIncludeNameIncludeDietPackFoodsIncludeDietPackFoodsSpec : Specification<DietPack>
{
    public GetUserPackageIncludeDietPackDietCategoriesIncludeNameIncludeDietPackFoodsIncludeDietPackFoodsSpec(double dailyCalorie, int dietCategoryId)
    {
        Query.AsNoTracking().Where(x => x.DailyCalorie == dailyCalorie && x.IsActive);
        Query.Include(x => x.DietPackDietCategories)
            .ThenInclude(x => x.DietCategory)
            .Where(x => x.DietPackDietCategories.Any(x => x.DietCategory.IsActive && x.DietCategoryId == dietCategoryId));
        Query.Include(x => x.Name);
        Query.Include(x => x.DietPackFoods).ThenInclude(x => x.Food).ThenInclude(x => x.Name);
        Query.Include(x => x.DietPackFoods).ThenInclude(x => x.MeasureUnit).ThenInclude(x => x.Name);



    }
}