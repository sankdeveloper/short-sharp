namespace ShortSharp.Extensions;

/// <summary>
/// 
/// </summary>
public static class DateTimeExtension
{
    private static readonly long InitialJavaScriptDateTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
    private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
    private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

    public static bool IsValid(this DateTime value)
    {
        return (value >= MinDate) && (value <= MaxDate);
    }

    public static DateTime AssumeUniversalTime(this DateTime dateTime)
    {
        return new DateTime(dateTime.Ticks, DateTimeKind.Utc);
    }

    public static long ToJavaScriptTicks(this DateTime dateTime)
    {
        DateTimeOffset utcDateTime = dateTime.ToUniversalTime();
        long javaScriptTicks = (utcDateTime.Ticks - InitialJavaScriptDateTicks) / (long)10000;
        return javaScriptTicks;
    }
}