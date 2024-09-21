
using FoodStuff.Domain.Entities.Translation;
using System.Collections.Generic;

namespace FoodStuff.Domain.Common
{
    public class IngredientModel : MeasureValuBase<double>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NamId { get; set; }
        public List<MeasureValuBase<double>> MeasureUnitList { get; set; }
    }
    public class IngMeasurModel
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int MeasureUnitId { get; set; }
        public Translation Translation { get; set; }
    }
}
