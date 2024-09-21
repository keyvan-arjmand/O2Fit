using System.Collections.Generic;
using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.Blogs.V1.Queries.GetAllBlog
{
    public class GetAllBlogQuery:IRequest<List<BlogDto>>
    {
        
    }
}