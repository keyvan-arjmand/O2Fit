namespace Identity.V2.DataCast.Models;

public partial class Country
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public string? FlagIcon { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Translation Name { get; set; } = null!;
}
