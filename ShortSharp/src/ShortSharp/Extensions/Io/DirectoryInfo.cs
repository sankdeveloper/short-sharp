// ReSharper disable once CheckNamespace
namespace ShortSharp.Extensions;

public static partial class Io
{
    /// <returns>@this as a DirectoryInfo.</returns>
    public static DirectoryInfo ToDirectoryInfo(this string @this)
    {
        return new DirectoryInfo(@this);
    }

    /// <summary>
    ///     A DirectoryInfo extension method that clears all files and directories in this directory.
    /// </summary>
    /// <param name="this">The obj to act on.</param>
    public static void Clear(this DirectoryInfo @this)
    {
        Array.ForEach(@this.GetFiles(), x => x.Delete());
        Array.ForEach(@this.GetDirectories(), x => x.Delete(true));
    }

    /// <summary>
    ///     A DirectoryInfo extension method that deletes the directories where.
    /// </summary>
    /// <param name="this">The obj to act on.</param>
    /// <param name="predicate">The predicate.</param>
    public static void DeleteDirectoriesWhere(this DirectoryInfo @this, Func<DirectoryInfo, bool> predicate)
    {
        @this
            .GetDirectories()
            .Where(predicate)
            .ForEach(x => x.Delete());
    }

    /// <summary>
    ///     A DirectoryInfo extension method that deletes the directories where.
    /// </summary>
    /// <param name="this">The obj to act on.</param>
    /// <param name="searchOption">The search option.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="searchPattern"></param>
    public static void DeleteDirectoriesWhere(
        this DirectoryInfo @this,
        Func<DirectoryInfo, bool> predicate,
        SearchOption searchOption,
        string searchPattern = "*.*")
    {
        @this
            .GetDirectories(searchPattern, searchOption)
            .Where(predicate)
            .ForEach(x => x.Delete());
    }

    /// <summary>
    ///     A DirectoryInfo extension method that deletes the files where.
    /// </summary>
    /// <param name="this">The obj to act on.</param>
    /// <param name="predicate">The predicate.</param>
    public static void DeleteFilesWhere(this DirectoryInfo @this, Func<FileInfo, bool> predicate)
    {
        @this
            .GetFiles()
            .Where(predicate)
            .ForEach(x => x.Delete());
    }

    /// <summary>
    ///     A DirectoryInfo extension method that deletes the files where.
    /// </summary>
    /// <param name="this">The obj to act on.</param>
    /// <param name="searchOption">The search option.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="searchPattern">default is "*.*"</param>
    public static void DeleteFilesWhere(
        this DirectoryInfo @this,
        Func<FileInfo, bool> predicate,
        SearchOption searchOption,
        string searchPattern = "*.*")
    {
        @this
            .GetFiles(searchPattern, searchOption)
            .Where(predicate)
            .ForEach(x => x.Delete());
    }

    /// <summary>
    ///     A DirectoryInfo extension method that deletes the older than.
    /// </summary>
    /// <param name="this">The obj to act on.</param>
    /// <param name="minDate"></param>
    /// <param name="searchOption">The search option.</param>
    /// <param name="searchPattern"></param>
    public static void DeleteOlderThan(
        this DirectoryInfo @this,
        DateTime minDate,
        SearchOption searchOption,
        string searchPattern = "*.*")
    {
        @this
            .GetFiles(searchPattern, searchOption)
            .Where(x => x.LastWriteTime < minDate)
            .ForEach(x => x.Delete());

        @this
            .GetDirectories(searchPattern, searchOption)
            .Where(x => x.LastWriteTime < minDate)
            .ForEach(x => x.Delete());
    }

    /// <summary>
    ///     Creates all directories and subdirectories in the specified @this if the directory doesn't already exists.
    ///     This methods is the same as FileInfo.CreateDirectory however it's less ambigues about what happen if the
    ///     directory already exists.
    /// </summary>
    /// <param name="this">The directory @this to create.</param>
    /// <returns>An object that represents the directory for the specified @this.</returns>
    /// ###
    /// <exception cref="T:System.IO.IOException">
    ///     The directory specified by <paramref name="this" /> is a file.-or-The
    ///     network name is not known.
    /// </exception>
    /// ###
    /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
    /// ###
    /// <exception cref="T:System.ArgumentException">
    ///     <paramref name="this" /> is a zero-length string, contains only
    ///     white space, or contains one or more invalid characters as defined by
    ///     <see
    ///         cref="F:System.IO.Path.InvalidPathChars" />
    ///     .-or-<paramref name="this" /> is prefixed with, or contains only a colon character (:).
    /// </exception>
    /// ###
    /// <exception cref="T:System.ArgumentNullException">
    ///     <paramref name="this" /> is null.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.PathTooLongException">
    ///     The specified @this, file name, or both exceed the system-
    ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file
    ///     names must be less than 260 characters.
    /// </exception>
    /// ###
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    ///     The specified @this is invalid (for example, it is on
    ///     an unmapped drive).
    /// </exception>
    /// ###
    /// <exception cref="T:System.NotSupportedException">
    ///     <paramref name="this" /> contains a colon character (:) that
    ///     is not part of a drive label ("C:\").
    /// </exception>
    public static DirectoryInfo EnsureDirectoryExists(this DirectoryInfo @this)
    {
        try
        {
            return Directory.CreateDirectory(@this.FullName);
        }
        catch
        {
            return @this;
        }
    }

    public static IEnumerable<DirectoryInfo> EnumerateDirectories(
        this DirectoryInfo @this,
        string searchPattern = "*.*",
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        return Directory
            .EnumerateDirectories(@this.FullName, searchPattern, searchOption)
            .Select(x => new DirectoryInfo(x));
    }
    
    public static IEnumerable<DirectoryInfo> EnumerateDirectories(
        this DirectoryInfo @this, 
        string[] searchPatterns,
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        return searchPatterns
            .SelectMany(x => @this.GetDirectories(x, searchOption))
            .Distinct();
    }
    
        
    public static DirectoryInfo[] GetDirectories(
        this DirectoryInfo @this, 
        string[] searchPatterns,
        SearchOption searchOption)
    {
        return searchPatterns
            .SelectMany(x => @this.GetDirectories(x, searchOption))
            .Distinct()
            .ToArray();
    }
    
    public static FileInfo[] GetFiles(
        this DirectoryInfo @this, 
        string[] searchPatterns,
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        return searchPatterns
            .SelectMany(x => @this.GetFiles(x, searchOption))
            .Distinct()
            .ToArray();
    }
    
    public static FileInfo[] GetFilesWhere(
        this DirectoryInfo @this, 
        Func<FileInfo, bool> predicate,
        string searchPattern = "*.*",
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        return Directory
            .EnumerateFiles(@this.FullName, searchPattern, searchOption)
            .Select(x => new FileInfo(x))
            .Where(x => predicate(x))
            .ToArray();
    }
    
    public static FileInfo[] GetFilesWhere(
        this DirectoryInfo @this, 
        Func<FileInfo, bool> predicate,
        string[] searchPatterns, 
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        return searchPatterns
            .SelectMany(x => @this.GetFiles(x, searchOption))
            .Distinct()
            .Where(predicate)
            .ToArray();
    }
    
    public static long GetSize(this DirectoryInfo @this)
    {
        return @this
            .GetFiles("*.*", SearchOption.AllDirectories)
            .Sum(x => x.Length);
    }
    
    /// <summary>
    ///     Combines multiples string into a path.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="paths">A variable-length parameters list containing paths.</param>
    /// <returns>
    ///     The combined paths. If one of the specified paths is a zero-length string, this method returns the other path.
    /// </returns>
    public static string PathCombine(this DirectoryInfo @this, params string[] paths)
    {
        List<string> list = paths.ToList();
        list.Insert(0, @this.FullName);
        return Path.Combine(list.ToArray());
    }
    
    /// <summary>
    ///     Combines multiples string into a path.
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    /// <param name="paths">A variable-length parameters list containing paths.</param>
    /// <returns>
    ///     The combined paths as a FileInfo. If one of the specified paths is a zero-length string, this method returns
    ///     the other path.
    /// </returns>
    public static FileInfo PathCombineFile(this DirectoryInfo @this, params string[] paths)
    {
        List<string> list = paths.ToList();
        list.Insert(0, @this.FullName);
        return new FileInfo(Path.Combine(list.ToArray()));
    }
}