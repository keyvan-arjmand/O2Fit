using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure
{
    public static class UsernameValidate
    {
        public static bool IsPhone(string value)
        {
            bool _check = false;

            Regex regex = new Regex(@"^\d+$");

            if (regex.IsMatch(value))
            {
                _check = true;
            }

            return _check;
        }

        public static bool IsEmail(string value)
        {
            bool _check = false;

            Regex regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            if (regex.IsMatch(value))
            {
                _check = true;
            }

            return _check;
        }
    }
}
