using FoodStuff.Service.Models;
using System.Collections.Generic;

namespace FoodStuff.API.Models
{
    public class PutTipDto
    {
        public int FoodId { get; set; }
        public List<TipDto> Tips { get; set; }
    }
}