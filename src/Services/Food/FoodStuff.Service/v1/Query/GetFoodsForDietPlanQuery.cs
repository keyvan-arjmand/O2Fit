using FoodStuff.Common.Utilities;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodsForDietPlanQuery : IRequest<PageResult<GetFoodsForDietPlanViewModel>>
    {
        public string LanguageName { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public int CategoryId { get; set; }
        public List<int> NationalityIds { get; set; }
    }

    public class GetFoodsForDietPlanViewModel
    {

        public int FoodId { get; set; }

        public string FoodName { get; set; }

        public string NutrientValue { get; set; }
        public List<int> MeasureUnitIds { get; set; }

    }


    public class MeasureUnitViewModel
    {
        public string MeasureUnitName { get; set; }
        public int MeasureUnitId { get; set; }
    }

    public class FoodInDietCategory
    {

        public int Id { get; set; }

        public string NameTranslate { get; set; }

        public string NutrientValue { get; set; }

        public List<int> FoodMeals { get; set; }

        public int NationalityId { get; set; }
        public string NationalityName { get; set; }
        public int? NationalityParentId { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryParentId { get; set; }
        public float? CategoryPercent { get; set; }

        public int DietCategoryId { get; set; }
        public string DietCategoryName { get; set; }
        public int? DietCategoryParentId { get; set; }
    }

    public class NationalityViewModel
    {
        public int NationalityId { get; set; }
        public string NameTranslation { get; set; }
        public int? ParentId { get; set; }
    }

    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string NameTranslation { get; set; }
        public int? ParentId { get; set; }
        public float? Percent { get; set; }
    }

    public class DietCategoryViewModel
    {
        public int DietCategoryId { get; set; }
        public string NameTranslation { get; set; }
        public int? ParentId { get; set; }
    }

}
