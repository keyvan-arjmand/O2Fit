namespace Identity.V2.DataCast.Models;

public partial class City
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;
}
