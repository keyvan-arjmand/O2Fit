using FoodStuff.Domain.Entities.Diet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.DietQuery
{
   public class GetDietQuery : IRequest<List<UserTrackDietPack>>
    {
        public int UserId { get; set; }
    }
}
