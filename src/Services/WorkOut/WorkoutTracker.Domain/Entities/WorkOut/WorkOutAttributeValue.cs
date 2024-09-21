using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class WorkOutAttributeValue : BaseEntity
    {
        public int WorkOutAttributeId { get; set; }
        [ForeignKey(nameof(WorkOutAttributeId))]
        public WorkOutAttribute WorkOutAttribute { get; set; }
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation Translation { get; set; }
        public double BurnedCalories { get; set; }
    }
}
