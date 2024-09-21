using Identity.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Aggregates;

public class User : IdentityUser<int>, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public string ConfirmCode { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime InsertDate { get; set; } = DateTime.Now;
    public DateTime ConfirmCodeExpireTime { get; set; }
}