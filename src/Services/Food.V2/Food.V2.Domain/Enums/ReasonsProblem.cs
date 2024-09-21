namespace Food.V2.Domain.Enums;

public enum ReasonsProblem
{
    [Display(Name = "نام غذا/غلط املایی")]
    SpellingMistake = 0,
    [Display(Name = "اشکال در ارزش غذایی")]
    DeficiencyInNutritionalValue = 1,
    [Display(Name = "این غذا متناسب با جستجوی من نبود")]
    SearchDidNotMatch = 0,
    [Display(Name = "موارد دیگر")]
    Other = 0,

}