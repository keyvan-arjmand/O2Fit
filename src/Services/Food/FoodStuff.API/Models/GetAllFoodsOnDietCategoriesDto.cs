namespace FoodStuff.API.Models
{
    public class GetAllFoodsOnDietCategoriesDto
    {
        public int DietCategoryId { get; set; }
        public string LanguageName { get; set; }
        public int PageSize { get; set; }
        public int? Page { get; set; }
    }
}
