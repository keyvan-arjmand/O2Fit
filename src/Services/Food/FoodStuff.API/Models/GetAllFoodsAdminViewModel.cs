
namespace FoodStuff.API.Models
{
    public class GetAllFoodsAdminViewModel
    {
        public int FoodId { get; set; }
        public TranslationDto Name { get; set; }
        public long Code { get; set; }
        public bool IsActive { get; set; }
    }
}
