using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.GetFoodAlergy
{
   public class GetFoodAlergyQuery:IRequest<List<GetFoodAlergyQueryResult>>
    {
        public int UserId { get; set; }
        public string LanguageName { get; set; }
    }
}
