namespace Order.V2.Application.Common.Utilities;

public static class EnumServices
{
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}