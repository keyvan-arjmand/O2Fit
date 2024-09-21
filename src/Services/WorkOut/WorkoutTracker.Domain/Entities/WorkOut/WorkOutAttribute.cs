using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class WorkOutAttribute : BaseEntity
    {
        public int WorkOutId { get; set; }
        [ForeignKey(nameof(WorkOutId))]
        public WorkOut WorkOut { get; set; }
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation Translation { get; set; }
        public ICollection<WorkOutAttributeValue> WorkOutAttributeValue { get; set; }
    }
}
