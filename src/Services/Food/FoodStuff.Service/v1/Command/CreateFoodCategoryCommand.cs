using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace FoodStuff.Service.v1.Command
{
    public class CreateFoodCategoryCommand : IRequest<bool>
    {
        public int FoodId { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
