using System.Collections.Generic;
using FoodStuff.Domain.Entities.Food;
using MediatR;


namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodNationalityCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<int> NationalityIds { get; set; }
        public List<FoodNationality> FoodNationalities { get; set; }
    }
}

