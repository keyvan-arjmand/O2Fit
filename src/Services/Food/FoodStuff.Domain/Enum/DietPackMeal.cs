using System.ComponentModel.DataAnnotations;


namespace FoodStuff.Domain.Enum
{
    public enum DietPackMeal
    {
        [Display(Name = "صبحانه")]
        Breakfast = 0,

        [Display(Name = "میان وعده صبح")]
        MorningSnack = 1,

        [Display(Name = "ناهار")]
        Lunch = 2,

        [Display(Name = "میان وعده ظهر")]
        NoonSnacke = 3,

        [Display(Name = "شام")]
        Dinner = 4,

        [Display(Name = "میان وعده شب")]
        NightSnacke = 5
    }

}
