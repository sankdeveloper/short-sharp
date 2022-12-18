// ReSharper disable once CheckNamespace

namespace ShortSharp.Extensions;

public static partial class Io
{
    public static IEnumerable<FileInfo> EnumerateFiles(
        this DirectoryInfo @this,
        string searchPattern = "*.*",
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        return Directory
            .EnumerateFiles(@this.FullName, searchPattern, searchOption)
            .Select(x => new FileInfo(x));
    }

    public static IEnumerable<FileInfo> EnumerateFiles(
        this DirectoryInfo @this,
        string[] searchPatterns,
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        return searchPatterns
            .SelectMany(x => @this.GetFiles(x, searchOption))
            .Distinct();
    }

    public static FileInfo EnsureExists(this FileInfo @this)
    {
        try
        {
            if (!File.Exists(@this.FullName))
            {
                File.Create(@this.FullName).Dispose();
            }

            return @this;
        }
        catch
        {
            return @this;
        }
    }

    public static FileInfo ToFileInfo(this string filePath)
    {
        return new FileInfo(filePath);
    }
}