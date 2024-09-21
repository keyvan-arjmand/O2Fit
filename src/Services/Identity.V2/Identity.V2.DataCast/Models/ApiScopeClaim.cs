namespace Identity.V2.DataCast.Models;

public partial class ApiScopeClaim
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int ScopeId { get; set; }

    public virtual ApiScope Scope { get; set; } = null!;
}
