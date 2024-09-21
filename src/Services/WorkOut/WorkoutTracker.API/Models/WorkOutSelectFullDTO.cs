using Domain;
using WorkoutTracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Transactions;
using WebFramework.Api;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class WorkOutSelectFullDTO
    {
        public int Id { get; set; }
        public Translation.Translation Name { get; set; }
        public Translation.Translation Recommandation { get; set; }
        public Translation.Translation HowToDo { get; set; }
        public string IconUri { get; set; }
        public string ImageUri { get; set; }
        public string Video { get; set; }
        public string MaleVideo { get; set; }
        public string FemaleVideo { get; set; }
        public string Tag { get; set; }
        public double BurnedCalories { get; set; }
        public int Classification { get; set; }
        public int? CardioCategory { get; set; }
        public int? Level { get; set; }
        public int? Gender { get; set; }
        public List<int> BodyMuscles { get; set; }
        public int? TargetMuscle { get; set; }
        public List<AttributeFullModel> Attributes { get; set; }
    }
    public class AttributeFullModel
    {
        public int Id { get; set; }
        public Translation.Translation Name { get; set; }
        public List<AttributeSelectFullModel> AttributeValue { get; set; }
    }

    public class AttributeSelectFullModel
    {
        public int Id { get; set; }
        public Translation.Translation Name { get; set; }
        public double BurnedCalori { get; set; }
    }
}
