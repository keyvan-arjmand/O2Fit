using WorkoutTracker.Domain.Entities.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace WorkoutTracker.API.Models
{
    public class TranslationDto : BaseDto<TranslationDto,Translation>
    {
        public string Persian { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
    }
}
