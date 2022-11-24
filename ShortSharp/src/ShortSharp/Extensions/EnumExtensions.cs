using System.ComponentModel;
using System.Reflection;

namespace ShortSharp.Extensions;

/// <summary>
/// Enum Extensions
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Retrieve the description on the enum, e.g.
    /// [Description("Bright Pink")]
    /// BrightPink = 2,
    /// Then when you pass in the enum, it will retrieve the description
    /// </summary>
    /// <param name="enum">The Enumeration</param>
    /// <returns>A string representing the friendly name</returns>
    public static string GetDescription(this Enum @enum)
    {
        Type type = @enum.GetType();
        MemberInfo[] memInfo = type.GetMember(@enum.ToString());

        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return @enum.ToString();
    }
    
    /// <summary>
    /// Retrieves the multiple description on the enum
    /// </summary>
    public static IEnumerable<string> GetDescriptions(Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        var field = type.GetField(name!);
        var fieldAttributes = field!.GetCustomAttributes(typeof(DescriptionAttribute), true);
        
        var descriptions = new List<string>();
        foreach (DescriptionAttribute attribute in fieldAttributes)
        {
            descriptions.Add(attribute.Description);
        }
        return descriptions;
    }
    
    /// <summary>
    /// Converts Enum to ToDictionary with Key being Enum Property Name and Value being the Enum Value of that property
    /// </summary>
    public static Dictionary<string, string> ToDictionary(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException("type");
        }

        Dictionary<string, string> dictionary = new();
        foreach (Enum value in Enum.GetValues(type))
        {
            dictionary.Add(value.ToString(), GetDescription(value));
        }

        return dictionary;
    }
}