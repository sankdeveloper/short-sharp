using FluentAssertions;
using ShortSharp.Extensions;
using Xunit;

namespace Test.ShortSharp;

public class EnumerableExtensionTest
{
    private readonly List<string> _list = new()
    {
        "One",
        "Two",
        "Three",
        "Four",
        "Five",
    };

    [Fact]
    public void PickRandom()
    {
        var items = Enumerable.Range(0,100)
            .Select(x => _list.PickRandom())
            .ToList();
        foreach (var item in items)
        {
            items.Any(_ => _ == item).Should().BeTrue();
        }
    }
    
    [Fact]
    public void PickRandom2Items()
    {
        var item = _list.PickRandom(2).ToList();
        Assert.NotNull(item);
        item.Should().HaveCount(2);
    }
    
    [Fact]
    public void Shuffle()
    {
        var item = _list.Shuffle().ToList();
        item.Should().HaveCount(5);
    }
    
    [Fact]
    public void ShuffleNTimes()
    {
        var item = _list.Shuffle(50).ToList();
        item.Should().HaveCount(5);
    }
}