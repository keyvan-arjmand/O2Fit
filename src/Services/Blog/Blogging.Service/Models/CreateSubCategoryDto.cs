using Blogging.Domain.Enum;
using Blogging.Service.Dtos;
using Microsoft.AspNetCore.Http;

namespace Blogging.Service.Models
{
    public class CreateSubCategoryDto
    {
        public IFormFile Image { get; set; }
        public string AltImage { get; set; }
         public TranslationDto Title { get; set; }
        public int CategoryId { get; set; }
    }
}