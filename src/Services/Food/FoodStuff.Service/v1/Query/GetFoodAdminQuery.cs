using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Filter;
using MediatR;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodAdminQuery : IRequest<PageResult<FoodResultAdmin>>
    {
        public string LanguageName { get; set; }
        public FoodInputParameters foodInputParameters { get; set; }
        public int PageSize { get; set; }
        public int? Page { get; set; }
    }

    public class FoodResultAdmin
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string NutrientValue { get; set; }
        public TranslationName BrandName { get; set; }
        public int FoodType { get; set; }
        public long FoodCode { get; set; }
        public bool IsActive { get; set; }
    }
}
