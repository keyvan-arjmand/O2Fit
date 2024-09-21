using Blogging.Domain.Enum;
using Microsoft.AspNetCore.Http;
using TranslationDto =Blogging.Service.Dtos.TranslationDto;

namespace Blogging.API.Models.V1
{
    public class CreateBlog
    {
        public Service.Dtos.TranslationDto Title { get; set; }
        public Service.Dtos.TranslationDto Description { get; set; }
         public Service.Dtos.TranslationDto ShortDescription { get; set; }
        public BlogStatus Status { get; set; }
        public IFormFile Image { get; set; }
        public string AltImage { get; set; }
        public IFormFile Thumb { get; set; }
        public string AltThumb { get; set; }
        public IFormFile Banner { get; set; }
        public string AltBanner { get; set; }
        public string KeyWords { get; set; }
        public long Like { get; set; }
        public int SubCategoryId { get; set; }
        public bool FirstPagePost { get; set; }
    }
}