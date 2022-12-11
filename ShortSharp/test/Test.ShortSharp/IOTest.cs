using ShortSharp.Extensions.IO;

namespace Test.ShortSharp;

public class IoTest
{
    public void Test()
    {
        var folder = "./test";

        folder
            .ToDirectoryInfo()
            .EnsureDirectoryExists();
    }
}