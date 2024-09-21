namespace FoodStuff.Service.Models
{
    public class NationalityDto
    {
        public int Id { get; set; }
        public TranslationResultDto NameTranslation { get; set; }

        public int? ParentId { get; set; }
    }
}