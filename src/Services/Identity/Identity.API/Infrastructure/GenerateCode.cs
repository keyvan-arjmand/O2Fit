using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure
{
    public static class GenerateCode
    {
        public static string Number(int count)
        {
            var random = new Random();

            string _code = string.Empty;

            for (int i = 0; i < count; i++)
            {
                _code = String.Concat(_code, random.Next(10).ToString());
            }

            return _code;
        }
    }
}
