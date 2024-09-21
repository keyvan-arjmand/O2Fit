using FoodStuff.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query
{
    public class GetIngredientCalculateQuery : IRequest<SelectIngredient>
    {
        public List<IngMeasurModel> IngredientModels { get; set; }
    }
}
