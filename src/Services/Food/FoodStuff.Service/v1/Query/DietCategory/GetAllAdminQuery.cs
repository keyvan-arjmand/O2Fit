using System.Collections.Generic;
using Amazon.Runtime.Internal;
using FoodStuff.Common.Utilities;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v1.Query.DietCategory
{
    public class GetAllAdminQuery:IRequest<PageResult<DietCategoryResultDto>>
    {
        public int? Page { get; set; }
        public int PageSize { get; set; }
    }
}