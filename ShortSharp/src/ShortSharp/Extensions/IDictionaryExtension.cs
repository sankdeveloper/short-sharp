namespace ShortSharp.Extensions;

public static class DictionaryExtension
{
    /// <summary>
    ///     An IDictionary&lt;TKey,TValue&gt; extension method that adds if not contains key.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <param name="dictionary">The @this to act on.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool AddIfNotContainsKey<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue value)
    {
        if (dictionary.ContainsKey(key))
            return false;

        dictionary.Add(key, value);
        return true;
    }

    /// <summary>
    ///     Uses the specified functions to add a key/value pair to the IDictionary&lt;TKey, TValue&gt; if the key does
    ///     not already exist, or to update a key/value pair in the IDictionary&lt;TKey, TValue&gt;> if the key already
    ///     exists.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <param name="dictionary">The @this to act on.</param>
    /// <param name="key">The key to be added or whose value should be updated.</param>
    /// <param name="value">The value to be added or updated.</param>
    /// <returns>The new value for the key.</returns>
    public static TValue UpsertByKey<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        return dictionary[key];
    }

    /// <summary>
    ///     Adds a key/value pair to the IDictionary&lt;TKey, TValue&gt; if the key does not already exist.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <param name="dictionary">The @this to act on.</param>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value to be added, if the key does not already exist.</param>
    /// <returns>
    ///     The value for the key. This will be either the existing value for the key if the key is already in the
    ///     dictionary, or the new value if the key was not in the dictionary.
    /// </returns>
    public static TValue GetOrAdd<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue value)
    {
        if (dictionary.ContainsKey(key))
            return dictionary[key];

        dictionary.Add(new KeyValuePair<TKey, TValue>(key, value));
        return dictionary[key];
    }

    /// <summary>
    ///     An IDictionary&lt;TKey,TValue&gt; extension method that removes if contains key.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <param name="dictionary">The @this to act on.</param>
    /// <param name="key">The key.</param>
    public static void RemoveIfContainsKey<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary.Remove(key);
        }
    }
}