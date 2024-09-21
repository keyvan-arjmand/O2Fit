namespace Track.Application.Common.Utilities;

public static class Converter
{
    public static DateTime DateTimeFormat(this DateTime dateTime)
    {
        return DateTime.Parse(dateTime.ToString("MM/dd/yyyy"));
    }

    public static string DateTimeFormatString(this DateTime dateTime)
    {
        return dateTime.ToString("MM/dd/yyyy");
    }
}