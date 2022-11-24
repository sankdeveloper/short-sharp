using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ShortSharp.Extensions;

public static class StringExtensions
{
    private static readonly Regex IsWebUrlRegex = new(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.Compiled);
    
    [DebuggerStepThrough]
    public static bool IsCaseSensitiveEquals(this string value, string comparing)
    {
        return string.CompareOrdinal(value, comparing) == 0;
    }

    [DebuggerStepThrough]
    public static bool IsCaseInsensitiveEquals(this string value, string comparing)
    {
        return string.Compare(value, comparing, StringComparison.OrdinalIgnoreCase) == 0;
    }

    [DebuggerStepThrough]
    public static string? GetMD5Hash(this string value, bool toBase64 = false, bool unicode = false)
    {
        if (string.IsNullOrWhiteSpace(value))
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
        return !string.IsNullOrEmpty(value) && IsWebUrlRegex.IsMatch(value.Trim());
    }
    
    [DebuggerStepThrough]
    public static string UrlEncode(this string value)
    {
        return HttpUtility.UrlEncode(value);
    }

    [DebuggerStepThrough]
    public static string UrlDecode(this string value)
    {
        return HttpUtility.UrlDecode(value);
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
}