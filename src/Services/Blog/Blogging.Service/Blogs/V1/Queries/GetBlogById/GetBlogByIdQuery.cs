using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.Blogs.V1.Queries.GetBlogById
{
    public class GetBlogByIdQuery:IRequest<BlogDto>
    {
        public int Id { get; set; }
    }
}