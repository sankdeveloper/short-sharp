using System.Text;
using ShortSharp.Extensions;
using Xunit;

namespace Test.ShortSharp;

public class StringExtensionTest
{
    [Fact]
    public void ToMemoryStreamTest()
    {
        string s = "a";
        using var ms = s.ToMemoryStream(Encoding.UTF8);
        Assert.Equal(1, ms.Length);

        using var sr = new StreamReader(ms);
        Assert.Equal(s, sr.ReadToEnd());
    }

    [Fact]
    public void CheckNumbers()
    {
        Assert.True("123456".IsInteger());
        Assert.False("123G456".IsInteger());
        Assert.False("123456.1".IsInteger());
        Assert.False("".IsInteger());

        Assert.False("123G456".IsDouble());
        Assert.True("123456.1".IsDouble());
        Assert.False("123G456.1".IsDouble());
    }
    
    [Fact]
    public void IsWebUrl()
    {
        Assert.True("https://github.com/ctimmons".IsWebUrl());
        Assert.False("www.sanket.com".IsWebUrl());
        Assert.False("https://github".IsWebUrl());
        Assert.False("".IsWebUrl());
    }
    
    [Fact]
    public void StringComparison()
    {
        Assert.True("ThisIsTest".EqualsCaseSensitive("ThisIsTest"));
        Assert.False("ThisIsTest".EqualsCaseSensitive("thisistest"));
        
        Assert.True("ThisIsTest".EqualsCaseIgnore("thisistest"));
        Assert.True("ThisIsTest".EqualsCaseIgnore("THISISTEST"));
    }
}