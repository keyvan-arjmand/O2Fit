using FoodStuff.Domain.Entities.ViewModels;

namespace FoodStuff.API.Models.ViewModels
{
    public class GetAllIngredientViewModel
    {
        public int Id { get; set; }

        public TranslationViewModel Translation { get; set; }

        public string Code { get; set; }
    }
}
