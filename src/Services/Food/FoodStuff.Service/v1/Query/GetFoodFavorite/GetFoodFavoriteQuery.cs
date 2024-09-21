using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Common;
using FoodStuff.Service.v1.Query.GetFoodFavorite;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query
{
  public class GetFoodFavoriteQuery:IRequest<PageResult<GetFoodFavoriteQueryResult>>
    {
        public int UserId { get; set; }
        public int PageSize { get; set; }
        public int? Page { get; set; }
        public string LanguageName { get; set; }
    }
}
