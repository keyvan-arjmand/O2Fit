using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.API.Models
{
    public class BodyMuscleDTO:BaseDto<BodyMuscleDTO,BodyMuscle>
    {
        public TranslationDto Name { get; set; }
    }
}
