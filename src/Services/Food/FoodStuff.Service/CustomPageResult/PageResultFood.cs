using FoodStuff.Common.Utilities;

namespace FoodStuff.Service.CustomPageResult
{
    public class PageResultFood<T> : PageResult<T>
    {
        public int InactiveFoods { get; set; }
        public int ActiveFoods { get; set; }
    }
}
