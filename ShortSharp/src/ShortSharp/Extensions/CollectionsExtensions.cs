namespace ShortSharp.Extensions;

public static class CollectionsExtensions
{
    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that adds only if the value satisfies the predicate.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="collection">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool AddIf<T>(this ICollection<T> collection, Func<T, bool> predicate, T value)
    {
        if (collection is null)
        {
            throw new ArgumentException($"{nameof(AddIf)}() failed due to unsatisfied functional conditions.",
                nameof(collection));
        }

        if (!predicate(value))
            return false;

        collection.Add(value);
        return true;
    }

    /// <summary>
    /// An ICollection&lt;T&gt; extension method that add value if the ICollection doesn't contains it already.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="collection">The @this to act on.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool AddIfNotContains<T>(this ICollection<T> collection, T value)
    {
        if (collection is null)
        {
            throw new ArgumentException($"{nameof(AddIfNotContains)}() failed due to unsatisfied functional conditions.",
                nameof(collection));
        }

        if (collection.Contains(value))
            return false;

        collection.Add(value);
        return true;
    }

    /// <summary>
    /// An ICollection&lt;T&gt; extension method that removes if.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="collection">The @this to act on.</param>
    /// <param name="value">The value.</param>
    /// <param name="predicate">The predicate.</param>
    public static bool RemoveIf<T>(this ICollection<T> collection, Func<T, bool> predicate, T value)
    {
        if (collection is null)
        {
            throw new ArgumentException($"{nameof(RemoveIf)}() failed due to unsatisfied functional conditions.",
                nameof(collection));
        }

        if (predicate(value))
        {
            collection.Remove(value);
            return true;
        }
        return false;
    }

    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that adds a range to 'values'.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="collection">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void AddRange<T>(this ICollection<T> collection, params T[] values)
    {
        if (collection is null)
        {
            throw new ArgumentException($"{nameof(AddRange)}() failed due to unsatisfied functional conditions.",
                nameof(collection));
        }

        foreach (T value in values)
        {
            collection.Add(value);
        }
    }
    
    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that adds a collection of objects to the end of this collection only
    ///     for value who satisfies the predicate.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="collection">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void AddRangeIf<T>(this ICollection<T> collection, Func<T, bool> predicate, params T[] values)
    {
        if (collection is null)
        {
            throw new ArgumentException($"{nameof(AddRangeIf)}() failed due to unsatisfied functional conditions.",
                nameof(collection));
        }

        foreach (T value in values)
        {
            if (predicate(value))
            {
                collection.Add(value);
            }
        }
    }
    
    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that removes the range.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="collection">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void RemoveRange<T>(this ICollection<T> collection, params T[] values)
    {
        if (collection is null)
        {
            throw new ArgumentException($"{nameof(RemoveRange)}() failed due to unsatisfied functional conditions.",
                nameof(collection));
        }
        
        foreach (T value in values)
        {
            collection.Remove(value);
        }
    }
    
    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that removes range item that satisfy the predicate.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="collection">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    public static void RemoveRangeIf<T>(this ICollection<T> collection, Func<T, bool> predicate, params T[] values)
    {
        if (collection is null)
        {
            throw new ArgumentException($"{nameof(RemoveRangeIf)}() failed due to unsatisfied functional conditions.",
                nameof(collection));
        }
        
        foreach (T value in values)
        {
            if (predicate(value))
            {
                collection.Remove(value);
            }
        }
    }
    
    /// <summary>
    ///     An ICollection&lt;T&gt; extension method that removes value that satisfy the predicate.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="collection">The @this to act on.</param>
    /// <param name="predicate">The predicate.</param>
    public static void RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        if (collection is null)
        {
            throw new ArgumentException($"{nameof(RemoveWhere)}() failed due to unsatisfied functional conditions.",
                nameof(collection));
        }
        
        List<T> list = collection.Where(predicate).ToList();
        foreach (T item in list)
        {
            collection.Remove(item);
        }
    }
}