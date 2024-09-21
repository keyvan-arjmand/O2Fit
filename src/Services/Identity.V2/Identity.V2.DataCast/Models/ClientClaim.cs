namespace Identity.V2.DataCast.Models;

public partial class ClientClaim
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Value { get; set; } = null!;

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}
