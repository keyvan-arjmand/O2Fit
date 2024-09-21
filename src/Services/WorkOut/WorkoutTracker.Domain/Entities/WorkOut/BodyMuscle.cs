using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class BodyMuscle : BaseEntity
    {
        public int NameId { get; set; }
        [ForeignKey("NameId")]
        public Translation.Translation Translation { get; set; }
        public ICollection<WorkoutBodyMuscles> WorkoutBodyMuscles { get; set; }
    }
}
