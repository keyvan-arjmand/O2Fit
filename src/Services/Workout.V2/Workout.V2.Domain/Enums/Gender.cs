using System.ComponentModel.DataAnnotations;

namespace Workout.V2.Domain.Enums
{
    public enum Gender
    {
        [Display(Name = "زن")]
        Female = 0, 

        [Display(Name = "مرد")]
        Male = 1,
        
        [Display(Name = "هر دو")]
        Both = 2
    }
}
