namespace Identity.V2.DataCast.Models;

public partial class ClientCorsOrigin
{
    public int Id { get; set; }

    public string Origin { get; set; } = null!;

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}
