namespace Identity.V2.DataCast.Models;

public partial class ApiScopeProperty
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public int ScopeId { get; set; }

    public virtual ApiScope Scope { get; set; } = null!;
}
