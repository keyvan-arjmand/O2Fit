using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStuff.API.Models
{
    public class TestResult
    {
        public int count { get; set; }
        public TimeSpan duration { get; set; }
       public List<string> CodeList { get; set; }
    }
}
