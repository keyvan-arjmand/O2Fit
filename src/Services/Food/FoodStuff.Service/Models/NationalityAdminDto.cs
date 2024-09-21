namespace FoodStuff.Service.Models
{
    public class NationalityAdminDto
    {
        public int Id { get; set; }
        public TranslationResultDto NameTranslation { get; set; }

        public int? ParentId { get; set; }
    }
}