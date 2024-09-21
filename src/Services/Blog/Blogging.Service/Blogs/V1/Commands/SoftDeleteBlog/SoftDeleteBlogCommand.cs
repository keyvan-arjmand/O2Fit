using MediatR;

namespace Blogging.Service.Blogs.V1.Commands.SoftDeleteBlog
{
    public class SoftDeleteBlogCommand:IRequest
    {
        public int Id { get; set; }
    }
}