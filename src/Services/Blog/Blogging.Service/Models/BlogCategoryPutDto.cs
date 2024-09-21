using Microsoft.AspNetCore.Http;

namespace Blogging.Service.Models
{
    public class BlogCategoryPutDto
    {
        public int CategoryId { get; set; }
        public TranslationNewDto Name { get; set; }
        public bool IsInPage { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUri { get; set; }
    }
}
