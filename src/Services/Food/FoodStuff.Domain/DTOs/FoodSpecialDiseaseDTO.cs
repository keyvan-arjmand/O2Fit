using FoodStuff.Domain.Enum;
using System.Collections.Generic;

namespace FoodStuff.Domain.DTOs
{
    public class FoodSpecialDiseaseDTO
    {
        public int FoodId { get; set; }
        public List<SpecialDisease> SpecialDiseases { get; set; }
        public bool IsDiseases { get; set; }
    }
}
