using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class TempOldRecipePersian
{
    public int? FoodId { get; set; }

    public int? TranslationId { get; set; }

    public string? PersianText { get; set; }

    public string? ArabicText { get; set; }

    public string? EnglishText { get; set; }

    public bool? IsDone { get; set; }

    public int Id { get; set; }
}
