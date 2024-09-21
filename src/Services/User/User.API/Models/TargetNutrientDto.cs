using System.Collections.Generic;

namespace User.API.Models
{
    public class TargetNutrientDto
    {
        public int UserId { get; set; }
        public List<double> TargetNutrient { get; set; }
    }
}