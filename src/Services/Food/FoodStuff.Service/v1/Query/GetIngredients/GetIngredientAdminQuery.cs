using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Query.GetIngredients
{
    public class GetIngredientAdminQuery : IRequest<List<IngSearchResultDTO>>
    {
        public string name { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public string LanguageName { get; set; }
    }
}
