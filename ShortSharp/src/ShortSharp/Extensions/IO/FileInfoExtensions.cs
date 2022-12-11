namespace ShortSharp.Extensions.IO;

public static partial class IoExtensions
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