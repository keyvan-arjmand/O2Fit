using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.GetUserMealsByDate
{
  public  class GetUserMealsByDateQuery:IRequest<List<UserTrackFoodModelDTO>>
    {
        public string LanguageName { get; set; }
        public int userId { get; set; }
        public DateTime dateTime { get; set; }
    }
}
