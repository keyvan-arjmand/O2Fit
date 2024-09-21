using System.Collections.Generic;
using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.Blogs.V1.Queries.GetBlogBySubCategory
{
    public class GetBlogBySubCategoryQuery:IRequest<List<BlogDto>>
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}