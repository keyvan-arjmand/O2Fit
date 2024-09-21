using System.Collections.Generic;
using Blogging.Service.Dtos;
using MediatR;

namespace Blogging.Service.BlogCategories.V1.Queries.GetAllCategory
{
    public class GetAllCategoryQuery:IRequest<List<CategoryDto>>
    {
        
    }
}