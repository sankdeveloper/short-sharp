using FluentAssertions;
using ShortSharp.Extensions;

namespace Test.NugetPkg;

public class UnitTest
{
    [Fact]
    public void CanRunFromToRange()
    {
        //Arrange
        List<int> result = new();
        
        //Act
        foreach (var i in 0..10)
        {
            result.Add(i);
        }

        //Assert
        for (var index = 0; index < result.Count; index++)
        {
            var number = index;
            result.Should().HaveElementAt(index, number);
        }

        result.Should().HaveCount(11);
    }
    
    [Fact]
    public void CanRunUptoRange()
    {
        //Arrange
        List<int> result = new();
        
        //Act
        foreach (var i in ..10)
        {
            result.Add(i);
        }

        //Assert
        for (var index = 0; index < result.Count; index++)
        {
            var number = index;
            result.Should().HaveElementAt(index, number);
        }

        result.Should().HaveCount(11);
    }
    
    [Fact]
    public void CanRunInRange()
    {
        //Arrange
        List<int> result = new();
        
        //Act
        foreach (var i in 10)
        {
            result.Add(i);
        }

        //Assert
        for (var index = 0; index < result.Count; index++)
        {
            var number = index;
            result.Should().HaveElementAt(index, number);
        }

        result.Should().HaveCount(11);
    }
}