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
        inMemoryFile.WriteContent("o");
        inMemoryFile.WriteContent("n");
        inMemoryFile.WriteContent("e");
        var content = inMemoryFile.ReadContent();
        content.Should().Be("one");
        inMemoryFile.FileId.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void ReadWrite()
    {
        InMemoryFile inMemoryFile = new();
        inMemoryFile.WriteContent("o");
        inMemoryFile.WriteContent("n");
        inMemoryFile.WriteContent("e");
        inMemoryFile.WriteContent("two");
        var content = inMemoryFile.ReadContent();
        content = inMemoryFile.ReadContent();
        content.Should().Be("onetwo");
    }
    
    [Fact]
    public async Task WriteWriteAsync()
    {
        InMemoryFile inMemoryFile = new();
        await inMemoryFile.WriteContentAsync("o");
        await inMemoryFile.WriteContentAsync("n");
        await inMemoryFile.WriteContentAsync("e");
        var content = await inMemoryFile.ReadContentAsync();
        content.Should().Be("one");
    }

    [Fact]
    public async Task ReadWriteAsync()
    {
        InMemoryFile inMemoryFile = new();
        await inMemoryFile.WriteContentAsync("o");
        await inMemoryFile.WriteContentAsync("n");
        await inMemoryFile.WriteContentAsync("e");
        await inMemoryFile.WriteContentAsync("two");
        var content = await inMemoryFile.ReadContentAsync();
        content = await inMemoryFile.ReadContentAsync();
        content.Should().Be("onetwo");
    }
    
    [Fact]
    public async Task ClearFile()
    {
        InMemoryFile inMemoryFile = new();
        await inMemoryFile.WriteContentAsync("o");
        await inMemoryFile.WriteContentAsync("n");
        await inMemoryFile.WriteContentAsync("e");
        await inMemoryFile.WriteContentAsync("two");
        var content = await inMemoryFile.ReadContentAsync();
        content = await inMemoryFile.ReadContentAsync();
        content.Should().Be("onetwo");
        inMemoryFile.ClearFile();
        content = await inMemoryFile.ReadContentAsync();
        content.Should().Be("");
    }
}