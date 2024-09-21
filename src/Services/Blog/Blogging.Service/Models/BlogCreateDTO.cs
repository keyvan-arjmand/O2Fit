using Microsoft.AspNetCore.Http;
namespace Blogging.Service.Models
{
    public class BlogCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Keyword { get; set; }
        public bool FirstPagePost { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public string VideoUri { get; set; }
        ///File
        public IFormFile Thumb { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Banner { get; set; }
        public int SubCategoryId { get; set; }
    }
}
