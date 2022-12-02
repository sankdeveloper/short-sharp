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
        
        var str = _list.PickRandom();
        Assert.NotNull(item);
        str.Should().NotBeNull();
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
        var item = _list.Shuffle().Shuffle(50).ToList();
        item.Should().HaveCount(5);
    }
    
    [Fact]
    public void ForEachIEnumerable()
    {
        _list.AsEnumerable().ForEach(item =>
        {
            item.Should().NotBeNull();
        });
    }
    
    [Fact]
    public void JoinIEnumerableString()
    {
        var expected = _list.Aggregate((a, b) => a + "," + b);
        var result = _list.Join(",");

        expected.Should().Be(result);
    }
    
    [Fact]
    public async Task WhenAllAsync()
    {
        var asyncTasks = Enumerable.Range(0, 10)
            .Select(number=> Task<int>.Factory.StartNew(() =>
            {
                Task.Delay(5000); 
                return number;
            }));
        
        var result = await asyncTasks.WhenAllAsync();
        result.Should().HaveCount(10);
    }
    
    [Fact]
    public async Task WhenAllSequentialAsync()
    {
        var asyncTasks = Enumerable.Range(0, 10)
            .Select(number=> Task<int>.Factory.StartNew(() =>
            {
                Task.Delay(5000); 
                return number;
            }));

        var result = await asyncTasks.WhenAllSequentialAsync();

        var existingNumber = -1;
        result.ForEach(number =>
        {
            number.Should().BeGreaterThan(existingNumber);
            existingNumber = number;
        });
    }
    
    [Fact]
    public async Task WhenAllByChunkAsync()
    {
        var asyncTasks = Enumerable.Range(0, 10)
            .Select(number=> Task<int>.Factory.StartNew(() =>
            {
                Task.Delay(5000); 
                return number;
            }));

        var result = await asyncTasks.WhenAllByChunkAsync(2);

        var existingNumber = -1;
        result.ForEach(number =>
        {
            number.Should().BeGreaterThan(existingNumber);
            existingNumber = number;
        });
    }
}