using System.Text;

namespace ShortSharp.Utility;

/// <summary>
/// Create, Read, Write in memory file without creating on physical Disk based on UTF8-Encoding.
/// </summary>
public class InMemoryFile
{
    /// <summary>
    /// Get unique fileId of current file.
    /// </summary>
    public Guid FileId { get; }
    private readonly MemoryStream _memoryStream;

    public InMemoryFile()
    {
        _memoryStream = new MemoryStream();
        FileId = Guid.NewGuid();
    }

    /// <summary>
    /// Write/Append string content to file.
    /// </summary>
    /// <param name="content">file content to be written</param>
    public void WriteContent(string content)
    {
        byte[] textBytes = Encoding.UTF8.GetBytes(content);
        _memoryStream.Write(textBytes, 0, textBytes.Length);
    }

    /// <summary>
    /// Write/Append string content to file.
    /// </summary>
    /// <param name="content">file content to be written</param>
    public async Task WriteContentAsync(string content)
    {
        // Write some text to the MemoryStream
        byte[] textBytes = Encoding.UTF8.GetBytes(content);
        await _memoryStream.WriteAsync(textBytes);
    }

    /// <summary>
    /// Read string content of file.
    /// </summary>
    public string ReadContent()
    {
        _memoryStream.Position = 0;
        StreamReader reader = new(_memoryStream);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Read string content of file.
    /// </summary>
    public Task<string> ReadContentAsync()
    {
        _memoryStream.Position = 0;
        StreamReader reader = new(_memoryStream);
        return reader.ReadToEndAsync();
    }

    /// <summary>
    /// Clear content of the file.
    /// </summary>
    public void ClearFile()
    {
        _memoryStream.SetLength(0);
    }

    /// <summary>
    /// Save content to physical file
    /// </summary>
    public void SaveAsFile(FileInfo file)
    {
        File.WriteAllBytes(file.FullName, _memoryStream.ToArray());
    }

    /// <summary>
    /// Save content to physical file
    /// </summary>
    public async Task SaveAsFileAsync(FileInfo file)
    {
        await File.WriteAllBytesAsync(file.FullName, _memoryStream.ToArray());
    }

    /// <summary>
    /// Save content to physical file (e.g. D:\\test.txt)
    /// </summary>
    public void SaveAsFile(string filename)
    {
        File.WriteAllBytes(filename, _memoryStream.ToArray());
    }

    /// <summary>
    /// Save content to physical file (e.g. D:\\test.txt)
    /// </summary>
    public async Task SaveAsFileAsync(string filename)
    {
        await File.WriteAllBytesAsync(filename, _memoryStream.ToArray());
    }

    /// <summary>
    /// Get MemoryStream object of current state.
    /// </summary>
    public MemoryStream MemoryStream()
    {
        return _memoryStream;
    }
}