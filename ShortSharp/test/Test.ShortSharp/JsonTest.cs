using FluentAssertions;
using ShortSharp.Extensions;
using Xunit;

namespace Test.ShortSharp;

public class JsonTest : IDisposable
{
    private readonly string _fileName;
    public JsonTest()
    {
        _fileName = Path.GetTempPath() + Guid.NewGuid() + ".json"; 
    }

    [Fact]
    public void IsValidJson()
    {
        var validJson = "{\"name\": \"John\", \"age\": 30, \"city\": \"New York\"}";
        validJson.IsValidJson().Should().BeTrue();
        
        var invalidJson = "{\"name\": \"John\", \"age\": 30, \"city\": \"New York\"";
        invalidJson.IsValidJson().Should().BeFalse();
    }

    [Fact]
    public async Task TestJsonExtensions()
    {
        MyClass @object = new()
        {
            Property1 = "Peoperty1Value",
            Property2 = 2,
            Property3 = DateOnly.FromDateTime(DateTime.Now),
            Property4 = TimeOnly.FromDateTime(DateTime.Now),
            Property5 = DateTimeOffset.Now
        };
        var jsonString = @object.ToJson();
        jsonString.Should().BeOfType<string>();
        jsonString.Should().Contain("Peoperty1Value");

        await @object.ToJsonFileAsync(_fileName);
        File.Delete(_fileName);

        FileInfo file = _fileName.ToFileInfo().EnsureExists();
        await file.WriteJsonDataAsync(@object);
        var read = await file.ReadJsonFileAsync<MyClass>();
        File.Delete(file.FullName);
        read.Should().Be(@object);

        await _fileName.ToFileInfo().EnsureExists().WriteJsonDataAsync(@object);
        var read2 = await _fileName.ReadJsonFileAsync<MyClass>();
        read2.Should().Be(@object);
        File.Delete(_fileName);
    }
    
    public record MyClass
    {
        public string Property1 { get; set; }
        public int Property2 { get; set; }
        public DateOnly Property3 { get; set; }
        public TimeOnly Property4 { get; set; }
        public DateTimeOffset Property5 { get; set; }
    }

    public void Dispose()
    {
        try
        {
            File.Delete(_fileName);
        }
        catch
        {
            // ignored
        }
    }
}