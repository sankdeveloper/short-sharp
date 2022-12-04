using FluentAssertions;
using ShortSharp.Extensions;
using Xunit;

namespace Test.ShortSharp;

public class DictionaryTest
{
    private IDictionary<string, string> _dictionary = new Dictionary<string, string>();
    
    [Fact]
    public void DictionaryTest_()
    {
        _dictionary.AddIfNotContainsKey("one", "value-one");
        _dictionary.ContainsKey("one").Should().BeTrue();
        
        _dictionary.UpsertByKey("one", "value-1");
        _dictionary["one"].Should().Be("value-1");

        _dictionary.GetOrAdd("one", "get-or-add");
        _dictionary["one"].Should().Be("value-1");

        _dictionary.GetOrAdd("two", "value-2");
        _dictionary["two"].Should().Be("value-2");

        _dictionary.RemoveIfContainsKey("one");
        _dictionary.ContainsKey("one").Should().BeFalse();

        _dictionary.RemoveIfContainsKey("one");
        _dictionary.ContainsKey("one").Should().BeFalse();
    }
}