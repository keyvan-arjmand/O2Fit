using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Command.DeletePersonalFoods
{
   public class DeletePersonalFoodCommand:IRequest
    {
        public int Id { get; set; }
    }
}
