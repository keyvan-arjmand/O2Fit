namespace Identity.V2.DataCast.Models;

public partial class Translation
{
    public int Id { get; set; }

    public string? Persian { get; set; }

    public string? English { get; set; }

    public string? Arabic { get; set; }

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
