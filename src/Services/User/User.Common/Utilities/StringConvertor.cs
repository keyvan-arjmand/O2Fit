using System;
using System.Collections.Generic;
using System.Text;

namespace User.Common.Utilities
{
  public static class StringConvertor
    {
        public static string DoubleToString(List<double> numbers)
        {
            string result = "";
            foreach (var item in numbers)
            {
                if (result != "")
                {
                    result = result + "," + item.ToString();
                }
                else
                {
                    result = item.ToString();
                }

            }
            return result;
        }
        public static List<double> ToNumber(string value)
        {
            var numbers = value.Split(',');
            List<double> result = new List<double>();
            foreach (var item in numbers)
            {
                result.Add(Convert.ToDouble(item));
            }
            return result;
        }

        public static List<int> Toint(string value)
        {
            var numbers = value.Split(',');
            List<int> result = new List<int>();
            foreach (var item in numbers)
            {
                result.Add(Convert.ToInt32(item));
            }
            return result;
        }
    }
}
