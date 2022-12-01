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
    
    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action) {
        foreach (var item in sequence) 
            action(item);
    }
}