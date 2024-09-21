namespace Food.V2.Domain.Enums
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
        NoonSnack = 3,

        [Display(Name = "شام")]
        Dinner = 4,

        [Display(Name = "میان وعده شب")]
        NightSnack = 5
    }

}
