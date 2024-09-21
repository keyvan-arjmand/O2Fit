using System.Collections.Generic;
using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.SubCategories.V1.Queries.GetAllSubCategory
{
    public class GetAllSubCategoryQuery:IRequest<List<SubCategoryBlogDto>>
    {
        
    }
}