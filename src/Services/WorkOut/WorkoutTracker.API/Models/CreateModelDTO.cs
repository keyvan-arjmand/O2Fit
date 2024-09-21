using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebFramework.Api;
using WorkoutTracker.Domain.Entities.WorkOut;

namespace WorkoutTracker.API.Models
{
    public class CreateModelDTO : BaseDto<CreateModelDTO, CreateModel>
    {
        public WorkOutDTO WorkOutDTO { get; set; }

        [BindProperty]
        public TranslationNonReqired Attr1 { get; set; }

        [BindProperty]
        public TranslationNonReqired Attr2 { get; set; }

        [BindProperty]
        public List<BodyMuscle> BodyMusclesList { get; set; }

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
        public class BufferedSingleFileUploadDb
        {
            public IFormFile workoutImg { get; set; }
            public IFormFile workoutIcon { get; set; }
            public IFormFile GeneralV { get; set; }
            public IFormFile MenV { get; set; }
            public IFormFile WomenV { get; set; }
        }
        public class TranslationNonReqired
        {
            public int Id { get; set; }

            public string Persian { get; set; }

            public string English { get; set; }

            public string Arabic { get; set; }
        }

    }
}
