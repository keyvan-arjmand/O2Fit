using System.Collections.Generic;
using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.SubCategories.V1.Queries.GetByCategoryId
{
    public class GetByCategoryIdQuery:IRequest<List<SubCategoryBlogDto>>
    {
        public int Id { get; set; }
    }
}