using Blogging.Domain.Enum;
using MediatR;

namespace Blogging.Service.Blogs.V1.Commands.UpdateBlogStatus
{
    public class UpdateBlogStatusCommand:IRequest
    {
        public int Id { get; set; }
        public BlogStatus Status { get; set; }
    }
}