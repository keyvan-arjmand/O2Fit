
using Common.Enums.TypeEnums;

namespace Discount.Application.Dtos;

public class DiscountPackageDto
{
    public string Id { get; set; } = string.Empty;
    public string PackageId { get; set; }  = string.Empty;
    public int Percent { get; set; }
    public string PackageType { get; set; } = string.Empty;
}