using System;
using Blogging.Domain.Entities.Blogs;
using WebFramework.Api;

namespace Blogging.API.Models
{
    public class BlogDTO : BaseDto<BlogDTO, Blog, int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public DateTime InsertDate { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public string Keyword { get; set; }
        public bool FirstPagePost { get; set; }
        public int ViewCount { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        //file
        public string ThumbUri { get; set; }
        //file
        public string ImageUri { get; set; }
        //file
        public string BannerUri { get; set; }
        public string VideoUri { get; set; }
        public BlogCategoryDTO BlogCategory { get; set; }
    }
}
