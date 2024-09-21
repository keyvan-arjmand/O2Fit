using FoodStuff.Common.Utilities;
using MediatR;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
    public class GetAllDietPackQuery : IRequest<PageResult<GetAllDietPackDTO>>
    {

        public string Name { get; set; }
        public double DailyCalorie { get; set; }

        public int DietCategoryId { get; set; }
        public int? Meal { get; set; }

        public string Language { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
    }
}
