using Blogging.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Blogging.API.Models.V1
{
    public class CreateCategory
    {
        public Service.Dtos.TranslationDto Title { get; set; }
        public IFormFile Image { get; set; }
        public string AltImage { get; set; }
        public CategoryType Type { get; set; }

    }
}