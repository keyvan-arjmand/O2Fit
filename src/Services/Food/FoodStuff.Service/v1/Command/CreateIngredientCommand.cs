using FoodStuff.Domain.Entities.Translation;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Command
{
    public class CreateIngredientCommand : IRequest<Unit>
    {
        public Translation TranslationDto { get; set; }
        public List<double> NutrientValue { get; set; }
        public string Tag { get; set; }
        public string ThumbUri { get; set; }
        public string Code { get; set; }
        public string TagEn { get; set; }
        public string TagAr { get; set; }
        public int DefaultMeasureUnitId { get; set; }
        public List<int> MeasureUnitIds { get; set; }

    }
}
