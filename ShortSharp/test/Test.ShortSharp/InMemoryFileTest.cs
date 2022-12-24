using FluentAssertions;
using ShortSharp.Utility;
using Xunit;

namespace Test.ShortSharp;

public class InMemoryFileTest
{
    [Fact]
    public void WriteWrite()
    {
        InMemoryFile inMemoryFile = new();
        inMemoryFile.AppendContent("o");
        inMemoryFile.AppendContent("n");
        inMemoryFile.AppendContent("e");
        var content = inMemoryFile.ReadContent();
        content.Should().Be("one");
        inMemoryFile.FileId.Should().NotBe(Guid.Empty);
    }
    
    [Fact]
    public void WriteFresh()
    {
        InMemoryFile inMemoryFile = new();
        inMemoryFile.WriteContent("o");
        inMemoryFile.WriteContent("on");
        inMemoryFile.WriteContent("one");
        var content = inMemoryFile.ReadContent();
        content.Should().Be("one");
        inMemoryFile.AppendContent("two");
        content = inMemoryFile.ReadContent();
        content.Should().Be("onetwo");
    }

    [Fact]
    public void ReadWrite()
    {
        InMemoryFile inMemoryFile = new();
        inMemoryFile.AppendContent("o");
        inMemoryFile.AppendContent("n");
        inMemoryFile.AppendContent("e");
        inMemoryFile.AppendContent("two");
        var content = inMemoryFile.ReadContent();
        content = inMemoryFile.ReadContent();
        content.Should().Be("onetwo");
    }
    
    [Fact]
    public async Task WriteWriteAsync()
    {
        InMemoryFile inMemoryFile = new();
        await inMemoryFile.WriteContentAsync("o");
        await inMemoryFile.AppendContentAsync("n");
        await inMemoryFile.AppendContentAsync("e");
        var content = await inMemoryFile.ReadContentAsync();
        content.Should().Be("one");
    }

    [Fact]
    public async Task ReadWriteAsync()
    {
        InMemoryFile inMemoryFile = new();
        await inMemoryFile.WriteContentAsync("o");
        await inMemoryFile.AppendContentAsync("n");
        await inMemoryFile.AppendContentAsync("e");
        await inMemoryFile.AppendContentAsync("two");
        var content = await inMemoryFile.ReadContentAsync();
        content = await inMemoryFile.ReadContentAsync();
        content.Should().Be("onetwo");
    }
    
    [Fact]
    public async Task ClearFile()
    {
        InMemoryFile inMemoryFile = new();
        await inMemoryFile.WriteContentAsync("o");
        await inMemoryFile.AppendContentAsync("n");
        await inMemoryFile.AppendContentAsync("e");
        await inMemoryFile.AppendContentAsync("two");
        var content = await inMemoryFile.ReadContentAsync();
        content = await inMemoryFile.ReadContentAsync();
        content.Should().Be("onetwo");
        inMemoryFile.ClearFile();
        content = await inMemoryFile.ReadContentAsync();
        content.Should().Be("");
    }
}