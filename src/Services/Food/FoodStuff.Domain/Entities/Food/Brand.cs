using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class Brand : BaseEntity
    {
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation Translation { get; set; }
        public string LogoUri { get; set; }
        public string Address { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
