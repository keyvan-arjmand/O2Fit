using WorkoutTracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using WebFramework.Api;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.API.Models
{
    public class PersonalWorkOutDTO:BaseDto<PersonalWorkOutDTO,PersonalWorkOut>
    {
        public int UserId { get; set; }
        public double Calorie { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string _id { get; set; }
    }
}
