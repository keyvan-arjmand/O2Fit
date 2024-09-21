using System.Collections.Generic;
using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.Blogs.V1.Queries.GetSelected
{
    public class GetSelectedQuery:IRequest<List<BlogDto>>
    {
        
    }
}