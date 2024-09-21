using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebFramework.Api;
using WorkoutTracker.API.Models;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class WorkOutAttributeValueDTO : BaseDto<WorkOutAttributeValueDTO,WorkOutAttributeValue>
    {
        public int WorkOutAttributeId { get; set; }
        public TranslationDto Name { get; set; }
        public double BurnedCalories { get; set; }
    }
}
