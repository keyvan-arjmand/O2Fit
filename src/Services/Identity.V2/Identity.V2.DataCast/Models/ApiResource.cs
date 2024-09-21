namespace Identity.V2.DataCast.Models;

public partial class ApiResource
{
    public int Id { get; set; }

    public bool Enabled { get; set; }

    public string Name { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public string? AllowedAccessTokenSigningAlgorithms { get; set; }

    public bool ShowInDiscoveryDocument { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public DateTime? LastAccessed { get; set; }

    public bool NonEditable { get; set; }

    public virtual ICollection<ApiResourceClaim> ApiResourceClaims { get; set; } = new List<ApiResourceClaim>();

    public virtual ICollection<ApiResourceProperty> ApiResourceProperties { get; set; } = new List<ApiResourceProperty>();

    public virtual ICollection<ApiResourceScope> ApiResourceScopes { get; set; } = new List<ApiResourceScope>();

    public virtual ICollection<ApiResourceSecret> ApiResourceSecrets { get; set; } = new List<ApiResourceSecret>();
}
