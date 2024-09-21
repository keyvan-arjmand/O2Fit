using MediatR;

namespace Blogging.Service.SubCategories.V1.Commands.SoftDeleteSubBlog
{
    public class SoftDeleteSubBlogCommand : IRequest
    {
        public int Id { get; set; }
    }
}