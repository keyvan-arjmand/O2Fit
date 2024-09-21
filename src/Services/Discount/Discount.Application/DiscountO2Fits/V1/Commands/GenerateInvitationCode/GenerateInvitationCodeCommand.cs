using Common.Enums.TypeEnums;

namespace Discount.Application.DiscountO2Fits.V1.Commands.GenerateInvitationCode;

public class GenerateInvitationCodeCommand:IRequest
{
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public DiscountType DiscountType { get; set; }
}