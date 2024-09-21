using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebFramework.Api;
using WorkoutTracker.API.Models;

namespace WorkoutTracker.Domain.Entities.WorkOut
{
    public class WorkOutAttributeDTO : BaseDto<WorkOutAttributeDTO,WorkOutAttribute>
    {
        public int WorkOutId { get; set; }
        public TranslationDto Name { get; set; }
        public List<int> AttributeValues { get; set; }
    }
}
