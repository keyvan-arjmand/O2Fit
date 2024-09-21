using Blogging.Service.Dtos;
using Microsoft.AspNetCore.Http;

namespace Blogging.Service.Models
{
    public class UpdateSubBlogDto
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; } 
        public string AltImage { get; set; }
        public TranslationDto Title { get; set; }
        public int CategoryId { get; set; }
    }
}