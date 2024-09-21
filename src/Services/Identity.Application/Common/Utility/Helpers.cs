using System.Text.RegularExpressions;

namespace Identity.Application.Common.Utility;

public static partial class Helpers
{
    public static bool IsPhone(this string value)
    {
        return MyRegex().IsMatch(value);
    }

    [GeneratedRegex("^\\d+$")]
    private static partial Regex MyRegex();

    public static string GetConfirmCode()
    {
        var code = string.Empty;
        for (var i = 0; i < 6; i++)
        {
            code += new Random().Next(0, 9).ToString();
        }

        return code;
    }
    
}