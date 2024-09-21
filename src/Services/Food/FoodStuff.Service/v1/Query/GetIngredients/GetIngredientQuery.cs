using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.GetIngredients
{
    public class GetIngredientQuery:IRequest<List<IngSearchResultDTO>>
    {
        public string name { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public string LanguageName { get; set; }
    }
}
