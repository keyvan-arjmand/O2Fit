using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace FoodStuff.Service.Models
{
    public class CreateTipDto
    {
        public CreateTranslationDto Translation { get; set; }
    }
}