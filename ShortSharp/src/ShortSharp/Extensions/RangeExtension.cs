namespace ShortSharp.Extensions;

/// <summary>
/// 
/// </summary>
public static class RangeExtension
{
    /// <summary>
    /// Range iterator
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public static CustomIntEnumerator GetEnumerator(this Range range)
    {
        return new CustomIntEnumerator(range);
    }
    
    /// <summary>
    /// single digit integer iterator
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static CustomIntEnumerator GetEnumerator(this int number)
    {
        return new CustomIntEnumerator(new Range(0, number));
    }
}


/// <summary>
/// 
/// </summary>
public ref struct CustomIntEnumerator
{
    private int _current;
    private readonly int _end;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="range"></param>
    /// <exception cref="NotSupportedException"></exception>
    public CustomIntEnumerator(Range range)
    {
        if (range.End.IsFromEnd)
        {
            throw new NotSupportedException("Don't know where to stop the loop buddy :-) !!");
        }
        
        _current = range.Start.Value - 1;
        _end = range.End.Value;
    }
    
    /// <summary>
    /// Current iteration
    /// </summary>
    public int Current => _current;

    /// <summary>
    /// MoveNext iteration ?
    /// </summary>
    public bool MoveNext()
    {
        _current++;
        return _current <= _end;
    }
}