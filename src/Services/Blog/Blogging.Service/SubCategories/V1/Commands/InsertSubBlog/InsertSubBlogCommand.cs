using Blogging.Domain.Enum;
using MediatR;

namespace Blogging.Service.SubCategories.V1.Commands.InsertSubBlog
{
    public class InsertSubBlogCommand : IRequest
    {
        public string ImageName { get; set; }
        public string AltImage { get; set; } 
        public int CategoryId { get; set; }
        public int TitleId { get; set; } 
    }
}