using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class UserFavoriteWorkOut : BaseEntity
    {
        public int UserId { get; set; }
        public int WorkOutId { get; set; }
        [ForeignKey(nameof(WorkOutId))]
        public WorkOut WorkOut { get; set; }
        public string _id { get; set; }
    }
}
