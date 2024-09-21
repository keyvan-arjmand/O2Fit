using System;

namespace FoodStuff.Service.Models
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string _id { get; set; }
        public DateTime InsertDate { get; set; }
    }
}