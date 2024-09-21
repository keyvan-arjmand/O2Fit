using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class UserReportedFood
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Barcode { get; set; }

    public string? FirstImage { get; set; }

    public string? SecendImage { get; set; }

    public string? ThirdImage { get; set; }

    public DateTime DateCreate { get; set; }

    public int UserId { get; set; }
}
