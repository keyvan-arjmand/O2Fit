using Blogging.Service.Models;
using Microsoft.AspNetCore.Http;

namespace Blogging.API.Models
{
    public class BlogCategoryDTO
    {
        public TranslationNewDto Name { get; set; }
        public bool IsInPage { get; set; }
        public IFormFile Image { get; set; }
    }
}
