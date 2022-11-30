using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using static System.String;

namespace ShortSharp.Extensions;

public static class StringExtensions
{
    private static readonly Regex IsWebUrlRegex = new(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?",
        RegexOptions.Singleline | RegexOptions.Compiled);

    [DebuggerStepThrough]
    public static bool EqualsCaseSensitive(this string value, string compareTo)
    {
        return CompareOrdinal(value, compareTo) == 0;
    }

    [DebuggerStepThrough]
    public static bool EqualsCaseIgnore(this string value, string comparing)
    {
        return Compare(value, comparing, StringComparison.OrdinalIgnoreCase) == 0;
    }

    [DebuggerStepThrough]
    public static string? GetMD5Hash(this string value, bool toBase64 = false, bool unicode = false)
    {
        if (IsNullOrWhiteSpace(value))
        {
            throw new InvalidDataException();
        }

        using MD5 md5 = MD5.Create();
        byte[]? data = unicode ? Encoding.Unicode.GetBytes(value) : Encoding.ASCII.GetBytes(value);

        if (toBase64)
        {
            byte[] hash = md5.ComputeHash(data);
            return Convert.ToBase64String(hash);
        }

        return md5.ComputeHash(data)?.ToString()?.ToLower();
    }

    [DebuggerStepThrough]
    public static bool IsWebUrl(this string value)
    {
        return !IsNullOrEmpty(value) && IsWebUrlRegex.IsMatch(value.Trim());
    }

    [DebuggerStepThrough]
    public static string UrlEncode(this string value)
    {
        return HttpUtility.UrlEncode(value);
    }

    [DebuggerStepThrough]
    public static string UrlEncode(this string value, Encoding encoding)
    {
        return HttpUtility.UrlEncode(value, encoding);
    }

    [DebuggerStepThrough]
    public static string UrlDecode(this string value)
    {
        return HttpUtility.UrlDecode(value);
    }

    [DebuggerStepThrough]
    public static string UrlDecode(this string value, Encoding encoding)
    {
        return HttpUtility.UrlDecode(value, encoding);
    }

    [DebuggerStepThrough]
    public static string HtmlEncode(this string value)
    {
        return HttpUtility.HtmlEncode(value);
    }

    [DebuggerStepThrough]
    public static string HtmlDecode(this string value)
    {
        return HttpUtility.HtmlDecode(value);
    }

    /// <summary>
    /// Convert value to a MemoryStream, using a default Unicode encoding.
    /// <para>
    /// If <param>value</param> is null, no data is written to the memory stream.
    /// If you intended to write a null byte to the memory stream, pass "\0"
    /// in the <param>value</param> parameter.
    /// </para>
    /// </summary>
    public static MemoryStream ToMemoryStream(this string? value, Encoding encoding)
    {
        return new MemoryStream(encoding.GetBytes(value ?? Empty));
    }
    
    public static bool IsInteger(this string number)
    {
        /* Double.TryParse is used so num values with a larger range than Int64 can be handled. */
        double _ = 0.0;
        return double.TryParse(number, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out _);
    }
    
    public static bool IsDouble(this string number)
    {
        double _ = 0.0;
        return double.TryParse(number, NumberStyles.Float, NumberFormatInfo.CurrentInfo, out _);
    }
}