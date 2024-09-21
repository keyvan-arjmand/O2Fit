using FoodStuff.Domain.Entities.Food;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Command
{
    public class UpdateFoodSpecialDiseasesCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<int> SpecialDiseases { get; set; }
        public List<FoodSpecialDisease> OldSpecialDiseases { get; set; }
    }
}