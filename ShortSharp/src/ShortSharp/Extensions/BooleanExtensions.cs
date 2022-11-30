using System.Diagnostics;

namespace ShortSharp.Extensions;

public static class BooleanExtensions
{
    /// <summary>
    /// Returns Char 'Y' for true, 'N' for false.
    /// </summary>
    [DebuggerStepThrough]
    public static Char AsYOrN(this bool value)
    {
        return value ? 'Y' : 'N';
    }
    
    /// <summary>
    /// Returns string 'Yes' for true, 'No' for false.
    /// </summary>
    [DebuggerStepThrough]
    public static string AsYesOrNo(this bool value)
    {
        return value ? "Yes" : "No";
    }
    
    /// <summary>
    /// Returns int '1' for true, '0' for false.
    /// </summary>
    public static int As0Or1(this bool value)
    {
        return value ? 1 : 0;
    }
    
    /// <summary>
    /// Returns Char 'Zero' for true, 'One' for false.
    /// </summary>
    public static string AsZeroOrOne(this bool value)
    {
        return value ? "Zero" : "One";
    }
}