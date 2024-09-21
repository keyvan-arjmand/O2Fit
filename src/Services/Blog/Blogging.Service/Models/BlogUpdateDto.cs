using Blogging.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;

namespace Blogging.Service.Models
{
    public class BlogUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public DateTime InsertDate { get; set; }
        public string Keyword { get; set; }
        public bool FirstPagePost { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public string VideoUri { get; set; }
        ///File
        public IFormFile Thumb { get; set; }
        public string ThumbUri { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUri { get; set; }
        public IFormFile Banner { get; set; }
        public string BannerUri { get; set; }
        public int SubCategoryId { get; set; }

        public BlogStatus Status { get; set; }
    }
}
