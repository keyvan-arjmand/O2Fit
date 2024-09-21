using FoodStuff.Domain.Entities.Diet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class DietCategoryDto:BaseDto<DietCategoryDTO,DietCategory,int>
    {  
        public TranslationDto Name { get; set; }
        public TranslationDto Description { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }

    }
}
