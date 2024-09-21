using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
     public class WorkoutBodyMuscles:BaseEntity
    {
        public int WorkoutId { get; set; }
        [ForeignKey(nameof(WorkoutId))]
        public WorkOut WorkOut { get; set; }
        public int BodyMuscleId { get; set; }
        [ForeignKey(nameof(BodyMuscleId))]
        public BodyMuscle BodyMuscle { get; set; }

    }
}
