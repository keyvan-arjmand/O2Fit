using Amazon.Runtime.Internal;
using FoodStuff.Service.Models;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Command.RecipeTips
{
    public class UpdateRecipeTipCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<TipDto> Tips { get; set; }
    }
}