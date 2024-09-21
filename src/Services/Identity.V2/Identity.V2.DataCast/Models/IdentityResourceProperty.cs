﻿namespace Identity.V2.DataCast.Models;

public partial class IdentityResourceProperty
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public int IdentityResourceId { get; set; }

    public virtual IdentityResource IdentityResource { get; set; } = null!;
}
