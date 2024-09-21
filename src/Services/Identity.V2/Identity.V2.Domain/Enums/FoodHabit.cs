namespace Identity.V2.Domain.Enums;

public enum FoodHabit
{
    [Display(Name = "عادی")]
    Normal=1,

    [Display(Name = "گیاه ‌خوار")]
    Vegetarian=2,

    [Display(Name = "خام ‌گیاه‌ خوار")]
    Vegan=3,

    [Display(Name = "پاک‌ گیاه ‌خوار")]
    OvoLactoVegetarianism=4
}