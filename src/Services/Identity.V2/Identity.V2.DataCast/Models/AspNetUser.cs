namespace Identity.V2.DataCast.Models;

public partial class AspNetUser
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public int Language { get; set; }

    public DateTime? LastSeenTime { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime RegisterDate { get; set; }

    public string? ImageUri { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int CountryId { get; set; }

    public bool IsActive { get; set; }

    public string? ReferreralCode { get; set; }

    public string? ReferreralInviter { get; set; }

    public int StartOfWeek { get; set; }

    public int ReferreralCountBuy { get; set; }

    public string? ConfirmCode { get; set; }

    public DateTime ConfirmCodeExpireTime { get; set; }

    public int? NormalDietCategoryId { get; set; }

    public int? NutritionDietCategoryId { get; set; }
    public bool IsSendToNewSystem { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
