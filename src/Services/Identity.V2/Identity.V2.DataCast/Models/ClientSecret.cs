﻿namespace Identity.V2.DataCast.Models;

public partial class ClientSecret
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string Value { get; set; } = null!;

    public DateTime? Expiration { get; set; }

    public string Type { get; set; } = null!;

    public DateTime Created { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}
