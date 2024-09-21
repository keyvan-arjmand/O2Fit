using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Filter;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodByBarcodeQuery : IRequest<PageResult<FoodResult>>
    {
        public string LanguageName { get; set; }
        public FoodInputParameters foodInputParameters { get; set; }
        public int PageSize { get; set; }
        public int? Page { get; set; }
    }
}
