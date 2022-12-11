using System.Collections.Specialized;
using System.Web;
using static System.String;

namespace ShortSharp.Extensions;

public static class QueryStringExtension
{
    /// <summary>
    /// get entire querystring name/value collection
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static NameValueCollection QueryString(this Uri url)
    {
        return HttpUtility.ParseQueryString(url.Query);
    }

    /// <summary>
    /// get single querystring value with specified key
    /// </summary>
    /// <param name="url"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string? TryGetQueryStringParam(this Uri url, string key)
    {
        try
        {
            return url.QueryString()[key];
        }
        catch
        {
            return Empty;
        }
    }

    /// <summary>
    /// get single querystring value with specified key
    /// </summary>
    /// <param name="url"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string? TryGetQueryStringParam(this string url, string key)
    {
        try
        {
            return new Uri(url).QueryString()[key];
        }
        catch
        {
            return Empty;
        }
    }

    /// <summary>
    /// get entire querystring name/value collection
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static NameValueCollection QueryString(this string url)
    {
        return HttpUtility.ParseQueryString(new Uri(url).Query);
    }
}