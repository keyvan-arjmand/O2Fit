using System.ComponentModel.DataAnnotations;

namespace Workout.V2.Domain.Enums;

public enum TargetMuscles
{
    [Display(Name = "هیچ کدام")] NonOfThese = 0,
    [Display(Name = "چهار سر ران")] Quadriceps = 1,
    [Display(Name = "ساق")] Calves = 2,
    [Display(Name = "جلو بازو")] BicepsBrachii = 3,
    [Display(Name = "پشت بازو")] TricepsBrachii = 4,
    [Display(Name = "باسن")] Glutes = 5,
    [Display(Name = "شکم")] Abdominal = 6,
    [Display(Name = "کول")] Traps = 7,
    [Display(Name = "ساعد")] Forearm = 8,
    [Display(Name = "فیله کمر")] LowerBack = 9,
    [Display(Name = "سرشانه")] Shoulder = 10,
    [Display(Name = "سینه")] Chest = 11,
    [Display(Name = "همسترینگ")] Hamstring = 12,
    [Display(Name = "پشت")] Back = 13,
    [Display(Name = "زیربغل")] Lats = 14
}