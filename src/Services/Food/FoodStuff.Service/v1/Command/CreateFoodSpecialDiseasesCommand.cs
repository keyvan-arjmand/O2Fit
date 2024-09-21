using MediatR;
using System.Collections.Generic;


namespace FoodStuff.Service.v1.Command
{
    public class CreateFoodSpecialDiseasesCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public List<int> FoodSpecialDiseases { get; set; }
    }

}