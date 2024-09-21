namespace Identity.V2.DataCast.Models;

public partial class ClientPostLogoutRedirectUri
{
    public int Id { get; set; }

    public string PostLogoutRedirectUri { get; set; } = null!;

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}
