using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v2.Query.GetPersonalFoods
{
   public class GetPersonalFoodsQuery:IRequest<List<PersonalFoodSelectDTO>>
    {
        public int userId { get; set; }
    }
}
