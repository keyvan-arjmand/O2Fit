using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class WorkOut : BaseEntity
    {
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation Translation { get; set; }
        public Nullable<int> RecommandationId { get; set; }
        [ForeignKey(nameof(RecommandationId))]
        public Translation.Translation TranslationRecommandation { get; set; }
        public Nullable<int> HowToDoId { get; set; }
        [ForeignKey(nameof(HowToDoId))]
        public Translation.Translation TranslationHowToDo { get; set; }
        public string IconUri { get; set; }
        public string ImageUri { get; set; }
        public double BurnedCalories { get; set; }
        public string VideoUrl { get; set; }
        public Classification Classification { get; set; }
        public Nullable<CardioCategory> CardioCategory { get; set; }
        public Nullable<Gender> Gender { get; set; }
        public Nullable<int> TargetMuscle { get; set; }
        public Nullable<Level> Level { get; set; }
        public string Tag { get; set; }
        public ICollection<WorkoutBodyMuscles> WorkoutBodyMuscles { get; set; }
        public ICollection<WorkOutAttribute> WorkOutAttribute { get; set; }
        
    }
}
