using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class Translation
{
    public int Id { get; set; }

    public string? Persian { get; set; }

    public string? English { get; set; }

    public string? Arabic { get; set; }

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<DietCategory> DietCategoryDescriptions { get; set; } = new List<DietCategory>();

    public virtual ICollection<DietCategory> DietCategoryNames { get; set; } = new List<DietCategory>();

    public virtual ICollection<DietPack> DietPacks { get; set; } = new List<DietPack>();

    public virtual ICollection<Food> FoodNames { get; set; } = new List<Food>();

    public virtual ICollection<Food> FoodRecipes { get; set; } = new List<Food>();

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public virtual ICollection<MeasureUnit> MeasureUnits { get; set; } = new List<MeasureUnit>();

    public virtual ICollection<Nationality> Nationalities { get; set; } = new List<Nationality>();

    public virtual ICollection<RecipeCategore> RecipeCategores { get; set; } = new List<RecipeCategore>();

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();

    public virtual ICollection<Tip> Tips { get; set; } = new List<Tip>();
}
