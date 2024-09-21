using System.ComponentModel.DataAnnotations;

namespace Ordering.Domain.Enum
{
    public enum PackageType
    {
        [Display(Name = "کالری شماری")]
        CalorieCounting = 0,

        [Display(Name = "رژیم")]
        Diet = 1
    }
}
