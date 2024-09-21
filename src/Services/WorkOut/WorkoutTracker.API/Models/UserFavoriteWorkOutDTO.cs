using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.API.Models
{
    public class UserFavoriteWorkOutDTO:BaseDto<UserFavoriteWorkOutDTO,UserFavoriteWorkOut>
    {
        public int UserId { get; set; }
        public int WorkOutId { get; set; }
        public string _id { get; set; }
    }
}
