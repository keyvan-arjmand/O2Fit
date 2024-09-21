using Domain;
using System;

namespace FoodStuff.Domain.Entities.Food
{
    public class UserReportedFoods : BaseEntity<int>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string FirstImage { get; set; }
        public string SecendImage { get; set; }
        public string ThirdImage { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
