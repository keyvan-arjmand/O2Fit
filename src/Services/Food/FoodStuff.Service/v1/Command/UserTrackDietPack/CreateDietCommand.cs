using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Command.DietPack
{
   public class CreateDietCommand: IRequest<int>
    {
        public int UserId { get; set; }
        public double Calori { get; set; }

        public int nationalityId { get; set; }
        public int Category { get; set; }
        public DateTime StartDate { get; set; }
        public List<int> Alergies { get; set; }
    }
}
 