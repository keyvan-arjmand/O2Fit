using Amazon.Runtime.Internal;
using MediatR;
using System.Collections.Generic;
using FoodStuff.Domain.Entities.Translation;

namespace FoodStuff.Service.v1.Command.RecipeSteps
{
    public class CreateRecipeTipCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<Translation> TipCreate { get; set; }
    }
}