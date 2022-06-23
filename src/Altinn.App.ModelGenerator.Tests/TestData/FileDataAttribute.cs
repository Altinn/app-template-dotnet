using Xunit.Sdk;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Altinn.App.ModelGenerator.Tests;

public class FileDataAttribute : DataAttribute
{
    private readonly string[] Files;
    public FileDataAttribute(params string[] files)
    {
        Files = files;
    }
    public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
    {
        return new List<object[]>{Files.Select(f=>File.ReadAllText(Path.Join("TestData",f))).ToArray()};
    }
}