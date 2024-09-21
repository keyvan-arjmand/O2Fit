using System.Collections.Generic;

namespace FoodStuff.Domain.Entities.ViewModels
{
    public class GetByCodeIngredientAdminViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public bool IsAllergies { get; set; }
    }
    public class GetAllIngredientAdminViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
    }

    public class GetByIdIngredientAdminViewModel
    {
        public int Id { get; set; }
        public TranslationViewModel Name { get; set; }
        public bool IsFood { get; set; }
        public string ThumbUri { get; set; }
        public string Code { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public string TagEn { get; set; }
        public string TagAr { get; set; }
        public int DefaultMeasureUnitId { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<int> MeasureUnitIds { get; set; }
    }



    //public class Update
}
