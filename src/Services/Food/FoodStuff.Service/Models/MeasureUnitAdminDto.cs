using FoodStuff.Domain.Enum;

namespace FoodStuff.Service.Models
{
    public class MeasureUnitAdminDto
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Persian { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
        public MeasureUnitCategory MeasureUnitCategory { get; set; }
    }
}