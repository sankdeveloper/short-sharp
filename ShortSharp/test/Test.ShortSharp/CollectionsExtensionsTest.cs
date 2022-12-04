using FluentAssertions;
using ShortSharp.Extensions;
using Xunit;

namespace Test.ShortSharp;

public class CollectionsExtensionsTest
{
    private ICollection<string> _collection;

    public CollectionsExtensionsTest()
    {
        _collection = Enumerable.Range(0, 5).Select(number => $"string-{number}").ToList();
    }

    [Fact]
    public void AddIfRemoveIfTest()
    {
        _collection.AddIf(x => true, "AddIf-true");
        _collection.AddIf(x => false, "AddIf-false");

        _collection.Should().Contain("AddIf-true");
        _collection.Should().NotContain("AddIf-false");
    }

    [Fact]
    public void AddIfNotContainsTest()
    {
        _collection.AddIfNotContains("AddIfNotContains-added");
        _collection.AddIfNotContains("string-1");

        _collection.Should().Contain("AddIfNotContains-added");
        _collection.Count(_ => _.EqualsCaseIgnore("string-1")).Should().Be(1);
    }

    [Fact]
    public void RemoveIfTest()
    {
        _collection.RemoveIf(x => true, "AddIfNotContains-added");
        _collection.RemoveIf(x => false, "string-1");

        _collection.Should().NotContain("AddIfNotContains-added");
        _collection.Should().Contain("string-1");
    }

    [Fact]
    public void AddRangeTest()
    {
        int initialCount = _collection.Count;
        _collection.AddRange<string>("s1", "s2");
        int latterCount = _collection.Count;

        (latterCount - initialCount).Should().Be(2);
    }
    
    [Fact]
    public void RemoveRangeTest()
    {
        // arrange
        _collection.AddIfNotContains("s1");
        _collection.AddIfNotContains("s2");

        // act
        int initialCount = _collection.Count;
        _collection.RemoveRange<string>("s1", "s2");
        int latterCount = _collection.Count;

        //assert
        (initialCount - latterCount).Should().Be(2);
    }
    
    [Fact]
    public void RemoveWhereTest()
    {
        // arrange
        _collection.AddIfNotContains("string-1");
        
        // act
        _collection.RemoveWhere(x => x== "string-1");

        //assert
        _collection.Contains("string-1").Should().BeFalse();
    }
}