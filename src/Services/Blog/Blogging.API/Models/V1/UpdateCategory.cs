using Blogging.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Blogging.API.Models.V1
{
    public class UpdateCategory
    {
        public int Id { get; set; }
        public Service.Dtos.TranslationDto Title { get; set; } 
        public IFormFile Image { get; set; } 
        public string ImageName { get; set; } 
        public string AltImage { get; set; } 
        public CategoryType Type { get; set; }

    }
}