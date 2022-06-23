using System.Text.Json;
using System.Collections.Generic;
using Altinn.App.ModelGenerator;
using Xunit;

namespace Altinn.App.ModelGenerator.Tests;

public class LayoutToModelTests
{
    [Theory]
    [FileData("LayoutToModel/FormLayout.json", "LayoutToModel/Summary.json", "LayoutToModel/bindings.json", "LayoutToModel/model.cs")]
    public void TestGetModelDataBindings(string FormLayout, string summary, string expectedBindings, string expectedModel)
    {
        // while (!System.Diagnostics.Debugger.IsAttached)
        //         System.Threading.Thread.Sleep(500);
        //     System.Diagnostics.Debugger.Break();

        var bindings = LayoutToModel.GetDataModelBindings(new string[]{FormLayout, summary});
        Assert.Equal(JsonSerializer.Deserialize<List<string>>(expectedBindings), bindings);
        var generatedModel = LayoutToModel.Convert(bindings, "Altinn.App.Models.Model");
        Assert.Equal(
            expectedModel.Replace("\r\n","\n"), //Github actions has configured git to translate LF to CRLF
            generatedModel);
    }
    [Theory]
    [InlineData("Altinn.App.Models.KRT1226Gjenopprettingsplaner_M", "Altinn.App.Models", "KRT1226Gjenopprettingsplaner_M")]
    public void TestSplitNamespace(string fullClassName, string expectedNs, string expectedClassName)
    {
        var(ns, className) = LayoutToModel.SplitFullClassname(fullClassName);
        Assert.Equal(expectedNs, ns);
        Assert.Equal(expectedClassName, className);
    }
}