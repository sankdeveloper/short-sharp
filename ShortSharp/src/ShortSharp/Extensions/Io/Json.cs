using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace ShortSharp.Extensions;

public static class Json
{
    /// <summary>
    /// Converts a Type to a JSON string
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="object"></param>
    /// <param name="indented"></param>
    /// <returns></returns>
    public static string ToJson<T>(this T @object, bool indented = true)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(@object, options);
    }

    /// <summary>
    /// Converts a Type to a JSON string
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="object"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string ToJson<T>(this T @object, JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(@object, options);
    }

    /// <summary>
    /// Writes a Type to a JSON File
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="object"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static async Task ToJsonFileAsync<T>(this T @object, string fileName)
    {
        JsonSerializerOptions options = new() { WriteIndented = true };

        await using FileStream createStream = File.Create(fileName);
        await JsonSerializer.SerializeAsync(createStream, @object, options);
        await createStream.DisposeAsync();
    }

    /// <summary>
    /// Read a JSON File
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="file"></param>
    /// <returns></returns>
    public static async Task<T> ReadJsonFileAsync<T>(this FileInfo file)
    {
        if (!File.Exists(file.FullName))
        {
            throw new FileNotFoundException($"Invalid file path: {file.FullName}. Please use '.EnsureExists()'");
        }

        string jsonString = await File.ReadAllTextAsync(file.FullName);
        return JsonSerializer.Deserialize<T>(jsonString)!;
    }

    /// <summary>
    /// Writes a JSON File
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="file"></param>
    /// <param name="object">Json Class Source</param>
    /// <param name="options">Json options</param>
    /// <returns></returns>
    public static async Task WriteJsonDataAsync<T>(this FileInfo file, T @object, JsonSerializerOptions? options = null)
    {
        if (!File.Exists(file.FullName))
        {
            throw new FileNotFoundException($"Invalid file path: {file.FullName}. Please use '.EnsureExists()'");
        }

        JsonSerializerOptions fallBackOption = options ?? new() { WriteIndented = true };
        var content = JsonSerializer.Serialize(@object, fallBackOption);
        await File.WriteAllTextAsync(file.FullName, content);
    }

    /// <summary>
    /// Read a JSON File
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static async Task<T> ReadJsonFileAsync<T>(this string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"Invalid file path: {fileName}");
        }

        string jsonString = await File.ReadAllTextAsync(fileName);
        return JsonSerializer.Deserialize<T>(jsonString)!;
    }
}