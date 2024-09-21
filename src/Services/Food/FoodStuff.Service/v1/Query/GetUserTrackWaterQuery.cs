using FoodStuff.Domain.Entities.Food;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query
{
    public class GetUserTrackWaterQuery:IRequest<List<UserTrackWater>>
    {
        public int userId { get; set; }
        public DateTime dateTime { get; set; }

    }
}
