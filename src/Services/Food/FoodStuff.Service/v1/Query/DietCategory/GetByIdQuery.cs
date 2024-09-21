using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v1.Query.DietCategory
{
    public class GetByIdQuery:IRequest<DietCategoryResultDto>
    {
        public int Id { get; set; }
    }
}