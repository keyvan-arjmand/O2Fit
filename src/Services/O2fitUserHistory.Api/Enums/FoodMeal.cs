using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace O2fitUserHistory.Api.Enums
{
    public enum FoodMeal
    {
        [Display(Name = "صبحانه")]
        [Description("25")]
        Breakfast = 1,

        [Display(Name = "ناهار")]
        [Description("35")]
        Lunch = 2,

        [Display(Name = "میان وعده")]
        [Description("15")]
        Snacks = 3,

        [Display(Name = "شام")]
        [Description("25")]
        Dinner = 4,
    }
}
