using System;
using System.Collections.Generic;
using Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodStuff.Domain.Enum;

namespace FoodStuff.Domain.Entities.Food
{
    public class Recipe : BaseEntity
    {
        public RecipeStatus Status { get; set; }

        public DateTime DateInsert { get; set; }
        public int FoodId { get; set; }

        [ForeignKey(nameof(FoodId))]
        public Food Food { get; set; }
        public bool IsDelete { get; set; } = false;

        public ICollection<Tip> Tips { get; set; }
        public ICollection<RecipeStep> RecipeSteps { get; set; }

    }
}