using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain;
using FoodStuff.Domain.Entities.Diet;

namespace FoodStuff.Domain.Entities.Food
{
    public class Nationality : BaseEntity
    {
        
        public int NameId { get; set; }

        [ForeignKey(nameof(NameId))]
        public Translation.Translation NameTranslation { get; set; }

        public int? ParentId { get; set; }

        public  ICollection<FoodNationality> FoodNationalities { get; set; }
        public ICollection<DietPackNationality> DietPackNationalities { get; set; }
    }
}
