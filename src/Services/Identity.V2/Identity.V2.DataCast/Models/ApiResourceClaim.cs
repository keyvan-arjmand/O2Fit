namespace Identity.V2.DataCast.Models;

public partial class ApiResourceClaim
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int ApiResourceId { get; set; }

    public virtual ApiResource ApiResource { get; set; } = null!;
}
