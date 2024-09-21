namespace Identity.V2.DataCast.Models;

public partial class ClientRedirectUri
{
    public int Id { get; set; }

    public string RedirectUri { get; set; } = null!;

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}
