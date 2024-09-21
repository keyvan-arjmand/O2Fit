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
    public class WorkOutSelectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUri { get; set; }
        public string ImageUri { get; set; }
        public double BurnedCalories { get; set; }
        public string VideoUrl { get; set; }
        public int Classification { get; set; }
        public int Level { get; set; }
        public string Recommandation { get; set; }
        public string HowToDo { get; set; }
        public int? Gender { get; set; }
        public List<string> BodyMuscles { get; set; }
        public string TargetMuscle { get; set; }
        public List<AttributeModel> Attributes { get; set; }
    }
    public class AttributeModel
    {
        public SelectOption<int> WorkOutAttribute { get; set; }
        public List<AttributeSelectModel> WorkOutAttributeValue { get; set; }
    }

    public class AttributeSelectModel
    {
        public SelectOption<int> Attribute { get; set; }
        public double BurnedCalori { get; set; }
    }
}
