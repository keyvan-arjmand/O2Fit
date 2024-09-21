using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Enums;

public enum UseFulStatus
{
    [Display(Name = "مفید")]
    Useful=0,
    [Display(Name = "غیرمفید")]
    UnUseful=1
}