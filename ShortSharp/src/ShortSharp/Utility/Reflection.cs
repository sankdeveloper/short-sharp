using System.Reflection;

namespace ShortSharp.Utility;

public static class Reflection
{
    public static IEnumerable<string> GetPublicPropertyNames<T>()
    {
        return
            typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(pi => pi.Name);
    }

    public static Dictionary<string, object?> GetPublicPropertyValues<T>(object source)
    {
        var type = source.GetType();
        return
            GetPublicPropertyNames<T>()
                .ToDictionary(
                    propertyName => propertyName,
                    propertyName => type.GetProperty(propertyName)?.GetValue(source, null) ?? null);
    }
}