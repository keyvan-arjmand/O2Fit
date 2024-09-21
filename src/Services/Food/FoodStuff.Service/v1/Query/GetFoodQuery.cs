using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Filter;
using MediatR;
using System.Collections.Generic;


namespace FoodStuff.Service.v1.Query
{
    public class GetFoodQuery : IRequest<PageResult<FoodResult>>
    {
        public string LanguageName { get; set; }
        public FoodInputParameters foodInputParameters { get; set; }
        public int PageSize { get; set; }
        public int? Page { get; set; }
    }

    public class FoodResult
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string NutrientValue { get; set; }
        public TranslationName BrandName { get; set; }
        public int FoodType { get; set; }
        public long FoodCode { get; set; }
    }
    public class TranslationName
    {
        public string Persian { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
    }

    public class FoodVMQ
    {
        public int Id { get; set; }
        public int FoodType { get; set; }
        public long FoodCode { get; set; }
        public string NutrientValue { get; set; }
        public int? BrandId { get; set; }
        public string NameBrand { get; set; }
        public string NameTranslate { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public bool IsActive { get; set; }

    }

    public class FoodResultFoodCode
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string NutrientValue { get; set; }
        public TranslationName BrandName { get; set; }
        public int FoodType { get; set; }
        public long FoodCode { get; set; }
        public bool IsActive { get; set; }
    }
}
