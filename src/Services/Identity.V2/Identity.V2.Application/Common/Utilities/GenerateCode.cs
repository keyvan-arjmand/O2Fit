namespace Identity.V2.Application.Common.Utilities;

public class GenerateCode
{
    public static string Number(int count)
    {
        var random = new Random();

        string code = string.Empty;

        for (int i = 0; i < count; i++)
        {
            code = String.Concat(code, random.Next(10).ToString());
        }

        return code;
    }

    public static string Generate()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var referralCode = new string(
            Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

        return referralCode;
    }
}