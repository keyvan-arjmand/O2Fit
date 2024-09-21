using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FoodStuff.Common.Utilities
{
    public static class DateConvetor
    {
        public static string Getday(DateTime dateTime)
        {
          
         return dateTime.ToString("ddd",new CultureInfo("En"));
        }
    }
}
