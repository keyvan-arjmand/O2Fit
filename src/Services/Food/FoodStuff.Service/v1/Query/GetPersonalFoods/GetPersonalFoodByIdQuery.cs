using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.GetPersonalFoods
{
   public class GetPersonalFoodByIdQuery:IRequest<PersonalFoodSelectDTO>
    {
        public int Id { get; set; }
    }
}
