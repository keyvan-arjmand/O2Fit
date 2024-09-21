namespace Common.Enums.Foods;

public enum FoodHabit
{
    [Display(Name = "عادی")]
    Normal=0,

    [Display(Name = "گیاه‌خوار")]
    Vegetarian=1,

    [Display(Name = "خام‌گیاه‌خوار")]
    Vegan=2,

    [Display(Name = "پاک‌گیاه‌خوار")]
    OvoLactoVegetarianism=3
}