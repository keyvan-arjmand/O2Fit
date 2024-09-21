namespace Identity.V2.DataCast.Models;

public partial class ClientGrantType
{
    public int Id { get; set; }

    public string GrantType { get; set; } = null!;

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}
