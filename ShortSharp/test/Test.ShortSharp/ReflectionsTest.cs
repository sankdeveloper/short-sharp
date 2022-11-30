using ShortSharp.Utility;
using Xunit;

namespace Test.ShortSharp;

public class ReflectionsTest
{
    [Fact]
    public void GetProperties()
    {
        var props = Reflection.GetPublicPropertyNames<TestClass>().ToList();
        Assert.Equal(props[0], "Prop1");
        Assert.Equal(props[1], "Prop2");
    }
    
    [Fact]
    public void GetPropertyAndValues()
    {
        TestClass obj = new();
        var props = Reflection.GetPublicPropertyValues<TestClass>(obj);
        
        Assert.True(props.ContainsKey("Prop1"));
        Assert.True(props.ContainsKey("Prop2"));
        Assert.Equal(props["Prop1"], "Value One");
        Assert.Equal(props["Prop2"], 2);
    }
}

public class TestClass
{
    public string Prop1 { get; set; } = "Value One";
    public int Prop2 { get; set; } = 2;
}