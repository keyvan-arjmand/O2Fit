using MediatR;

namespace FoodStuff.Service.v2.Command.FoodMeasureUnits
{
    public class CreateFoodMeasureUnitWithConditionCommand : IRequest<Unit>
    {
        public int FoodId { get; set; }
        public int MeasureUnitId { get; set; }
    }
}