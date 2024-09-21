using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v1.Query.RecipeTips
{
    public class GetByIdRecipeTipQuery:IRequest<TipDto>
    {
        public int Id { get; set; }
    }
}