using System.Collections.Generic;
using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;

namespace FoodStuff.Service.v1.Query.RecipeTips
{
    public class GetAllRecipeTipQuery:IRequest<List<TipDto>>
    {
        
    }
}