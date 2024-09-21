using FluentValidation;
using WorkoutTracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.Domain.Entities.WorkOut;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.API.Models
{
    public class UserFavoriteWorkOutSelectDTO
    {
        public int WorkOutId { get; set; }
        public string Name { get; set; }
        public string _id { get; set; }
        public int UserId { get; set; }
        public Classification clasification { get; set; }
    }
}
