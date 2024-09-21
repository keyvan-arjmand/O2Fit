namespace Identity.V2.DataCast.Models;

public partial class IdentityResourceClaim
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int IdentityResourceId { get; set; }

    public virtual IdentityResource IdentityResource { get; set; } = null!;
}
