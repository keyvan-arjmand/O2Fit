using Blogging.Domain.Enum;
using MediatR;

namespace Blogging.Service.Blogs.V1.Commands.InsertBlog
{
    public class InsertBlogCommand:IRequest
    {
        public int TitleId { get; set; }
        public int DescriptionId { get; set; }
        public int ShortDescriptionId { get; set; }
        public BlogStatus Status { get; set; }
        public string Image { get; set; }
        public string AltImage { get; set; }
        public string Thumb { get; set; }
        public string AltThumb { get; set; }
        public string Banner { get; set; }
        public string AltBanner { get; set; }
        public long Like { get; set; }
        public int SubCategoryId { get; set; }
        public bool FirstPagePost { get; set; }
        public string KeyWords { get; set; }
    }
}