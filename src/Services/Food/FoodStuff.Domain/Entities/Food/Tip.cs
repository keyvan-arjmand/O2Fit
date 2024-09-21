using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace FoodStuff.Domain.Entities.Food
{
    public class Tip : BaseEntity
    {
        public int DescriptionId { get; set; }
        [ForeignKey(nameof(DescriptionId))]
        public Translation.Translation Translation { get; set; }
        public DateTime DateInsert { get; set; }
        public int? RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; }
        public bool IsDelete { get; set; } = false;

    }
}