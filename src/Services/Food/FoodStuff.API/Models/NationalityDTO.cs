using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class NationalityDTO : BaseDto<NationalityDTO, Nationality>
    {
        public TranslationDto NameTranslation { get; set; }

        public int? ParentId { get; set; }
    }

    public class DeleteNationalityDTO
    {
        public int Id { get; set; }
    }
}