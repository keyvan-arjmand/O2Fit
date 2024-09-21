using FoodStuff.Domain.Common;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v2.Command.FoodIngredients
{
    public class CreateFoodIngredientCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<IngMeasurModel> Ingredients { get; set; }

    }
}