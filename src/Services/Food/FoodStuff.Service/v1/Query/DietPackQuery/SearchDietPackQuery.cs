using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
    public class SearchDietPackQuery : IRequest<PageResult<DietPackSearchResultViewModel>>
    {
        public string Name { get; set; }
        public int FoodMeal { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public string Language { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public List<int> NationalityIds { get; set; }
        public double CalorieValue { get; set; }
        public double DailyCalorie { get; set; }
        public List<int> SpecialDiseases { get; set; }
        public List<int> IngredientAllergyIds { get; set; }

    }
}
