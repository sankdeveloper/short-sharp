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
}