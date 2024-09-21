using System.Text.RegularExpressions;

namespace Identity.V2.Application.Common.Utilities;

public class ValidateUsername
{
    public static bool IsPhone(string value)
    {
        bool check = false;
        //with bug
        Regex regex = new Regex(@"^\d+$");
        //without bug 
        //Regex regex = new Regex(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$");

        if (regex.IsMatch(value))
        {
            check = true;
        }

        return check;
    }

    public static bool IsEmail(string value)
    {
        bool check = false;

        Regex regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        if (regex.IsMatch(value))
        {
            check = true;
        }

        return check;
    }
}