namespace Food.V2.Domain.Enums
{
    public enum FoodMeal
    {
        [Display(Name = "صبحانه")]
        [Description("25")]
        Breakfast = 0,

        [Display(Name = "ناهار")]
        [Description("35")]
        Lunch = 1,

        [Display(Name = "میان وعده صبح")]
        [Description("5")]
        MorningSnacks = 2,

        [Display(Name = "شام")]
        [Description("25")]
        Dinner = 3,

        [Display(Name = "میان وعده ظهر")]
        [Description("5")]
        NoonSnacks = 4,

        [Display(Name = "میان وعده شب")]
        [Description("5")]
        NightSnacks = 5,

        #region Rozedari
        [Display(Name = "افطار")]
        [Description("")]
        Eftar = 6,

        [Display(Name = "میان وعده ی اول ")]
        [Description("")]
        Snack1 = 7,

        [Display(Name = "میان وعده ی دوم ")]
        [Description("")]
        Snack2 = 8,

        [Display(Name = "سحری")]
        [Description("")]
        Sahari = 9,

        #endregion


    }
}
