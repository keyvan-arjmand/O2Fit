using System;
using Domain;

namespace FoodStuff.Domain.Entities.Food
{
    public class Note : BaseEntity
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public string AppId { get; set; }
        public DateTime InsertDate { get; set; }
    }
}