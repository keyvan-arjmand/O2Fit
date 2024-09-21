using System;
using Blogging.Domain.Entities.Blogs;
using WebFramework.Api;

namespace Blogging.API.Models
{
    public class BlogSelectDTO : BaseDto<BlogSelectDTO, Blog, int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUri { get; set; }
        public string ThumbUri { get; set; }
        public DateTime InsertDate { get; set; }
        public string BannerUri { get; set; }
        public int LikeCount { get; set; }
        //public int CommentCount { get; set; }
        public string Keyword { get; set; }
        public string MainBanner { get; set; }
        public bool FirstPagePost { get; set; }
        // public int ViewCount { get; set; }
        public string Url { get; set; }
    }
}
