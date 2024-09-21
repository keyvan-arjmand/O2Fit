using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Transactions;
using WebFramework.Api;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class WorkOutDTO :BaseDto<WorkOutDTO,WorkOut>
    {
        public TranslationDto Name { get; set; }
        public TranslationDto Recommandation { get; set; }
        public TranslationDto HowToDo { get; set; }
        public string IconUri { get; set; }
        public string ImageUri { get; set; }
        public string VideoUrl { get; set; }
        public Nullable<double> BurnedCalories { get; set; }
        public double Version { get; set; }
        public Classification Classification { get; set; }
        public Nullable<CardioCategory> CardioCategory { get; set; }
        public Nullable<Gender> Gender { get; set; }
        public Nullable<int> TargetMuscle { get; set; }
        public Nullable<Level> Level { get; set; }
        public string Tag { get; set; }
        public List<int> BodyMuscles { get; set; }
        public List<AttributeDtoModel> Attributes { get; set; }
    }
    public class AttributeDtoModel
    {
        public int Id { get; set; }
        public TranslationDto Name { get; set; }
        public List<AttributeValueModel> AttributeValues { get; set; }
    }
    public class AttributeValueModel
    {
        public  int Id { get; set; }
        public TranslationDto Name { get; set; }
        public double BurnedCalories { get; set; }
    }
}
