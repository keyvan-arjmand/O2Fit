using Blogging.Domain.Enum;
using System;

namespace Blogging.Service.Models
{
    public class InsertBlogRedis
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ThumbUri { get; set; }
        public string ImageUri { get; set; }
        public string BannerUri { get; set; }
        public string VideoUri { get; set; }
        public string Url { get; set; }

        public DateTime InsertDate { get; set; }
        public string Keyword { get; set; }
        public bool FirstPagePost { get; set; }
        public string Language { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsDelete { get; set; } = false;
        public BlogStatus Status { get; set; }
    }
}
