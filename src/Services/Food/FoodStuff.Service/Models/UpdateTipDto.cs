using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class UpdateTipDto
    {
        public int Id { get; set; }
        public int RecipeId{ get; set; }
        public CreateTranslationDto Translation { get; set; }

    }
}