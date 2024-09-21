using System;
using System.Collections.Generic;

namespace FoodStuff.Common.Utilities
{
    public static class StringConvertor
    {
        public static string DoubleToString(List<double> numbers)
        {
            string result = "";
            foreach (var num in numbers)
            {
                double item = Math.Round(num, 6);
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
                result.Add(Math.Round(Convert.ToDouble(item), 6));
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
        public static string IntToString(List<int> numbers)
        {
            if (numbers != null)
            {
                string result = "";
                int number = 0;

                foreach (var item in numbers)
                {
                    if (number == 0)
                    {
                        result += $"{item}";
                    }
                    else
                    {
                        result += $",{item}";
                    }
                    number++;
                }

                return result;
            }

            return null;
        }
    }
}
