namespace ShortSharp.Extensions;

public static class EnumerableExtension
{
    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.Reverse().OrderBy(x => Guid.NewGuid()).Reverse();
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, int nTimes)
    {
        var sourceTmp = Enumerable.Empty<T>();
        foreach (var time in nTimes)
        {
            sourceTmp = source.Shuffle();
        }

        return sourceTmp;
    }

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).Single();
    }

    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
    {
        foreach (var item in sequence)
            action(item);
    }
    
    /// <summary>
    ///     Enumerates for each in this collection.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="sequence">The @this to act on.</param>
    /// <param name="action">The action.</param>
    /// <returns>An enumerator that allows foreach to be used to process for each in this collection.</returns>
    public static IEnumerable<T> ForEachWithReturn<T>(
        this IEnumerable<T> sequence, 
        Action<T> action)
    {
        T[] array = sequence.ToArray();
        foreach (T t in array)
        {
            action(t);
        }
        return array;
    }

    public static string Join(this IEnumerable<string> sequence, string separator = "")
    {
        return string.Join(separator, sequence);
    }

    public static async Task<IEnumerable<T>> WhenAllAsync<T>(this IEnumerable<Task<T>> tasks)
    {
        if (tasks is null)
            throw new ArgumentNullException(nameof(tasks));

        return await Task
            .WhenAll(tasks)
            .ConfigureAwait(false);
    }

    public static Task WhenAllAsync(this IEnumerable<Task> tasks)
    {
        if (tasks is null)
            throw new ArgumentNullException(nameof(tasks));

        return Task
            .WhenAll(tasks);
    }

    public static async Task<IEnumerable<T>> WhenAllSequentialAsync<T>(this IEnumerable<Task<T>> tasks)
    {
        if (tasks is null)
            throw new ArgumentNullException(nameof(tasks));

        var results = new List<T>();
        foreach (var task in tasks)
            results.Add(await task.ConfigureAwait(false));

        return results;
    }

    public static async Task WhenAllSequentialAsync(this IEnumerable<Task> tasks)
    {
        if (tasks is null)
            throw new ArgumentNullException(nameof(tasks));

        foreach (var task in tasks)
            await task.ConfigureAwait(false);
    }

    public static async Task<IEnumerable<T>> WhenAllByChunkAsync<T>(
        this IEnumerable<Task<T>> tasks,
        int chunkSize
    )
    {
        if (tasks is null)
            throw new ArgumentNullException(nameof(tasks));

        var results = new List<T>();
        foreach (var chunk in tasks.Chunk(chunkSize))
        {
            var chunkResults = await Task.WhenAll(chunk).ConfigureAwait(false);
            results.AddRange(chunkResults);
        }

        return results;
    }

    public static async Task WhenAllByChunkAsync(
        this IEnumerable<Task> tasks,
        int chunkSize
    )
    {
        if (tasks is null)
            throw new ArgumentNullException(nameof(tasks));

        foreach (var chunk in tasks.Chunk(chunkSize))
            await Task.WhenAll(chunk).ConfigureAwait(false);
    }
}