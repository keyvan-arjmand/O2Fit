namespace FoodStuff.Service.Models
{
    public class CreateIngredientDto
    {
        public double Value { get; set; }
        public int MeasureUnitId { get; set; }
        public CreateTranslationDto Translation { get; set; }
    }
}