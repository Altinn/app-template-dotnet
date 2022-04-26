using System.Linq;
using System.IO;
using System.Text.Json.Nodes;
using Microsoft.CodeAnalysis;

namespace Altinn.App.ModelGenerator
{
    [Generator]
    public class ModelSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // while (!System.Diagnostics.Debugger.IsAttached)
            //     System.Threading.Thread.Sleep(500);
            // System.Diagnostics.Debugger.Break();

            var layouts = context.AdditionalFiles.Where(af=>PathUtils.IsLayoutPath(af.Path)).ToDictionary(af=> new FileInfo(af.Path).Name, af=>af.GetText()!.ToString());
            var resources = context.AdditionalFiles.Where(af=>PathUtils.IsResourcePath(af.Path)).ToDictionary(af=> new FileInfo(af.Path).Name, af=>af.GetText()!.ToString());
            var applicationMetadata = context.AdditionalFiles.FirstOrDefault(af=>PathUtils.IsApplicationmetadata(af.Path))?.GetText()?.ToString();
            var settings = context.AdditionalFiles.FirstOrDefault(af=>PathUtils.IsSettings(af.Path))?.GetText()?.ToString();
            // TODO: run validations and create errors;
            
            var modelName = (JsonNode.Parse(applicationMetadata!)?["dataTypes"] as JsonArray)?.FirstOrDefault(dt=>dt?["appLogic"] != null)?["appLogic"]?["classRef"]?.GetValue<string>();
            // modelName = "Altinn.App.Models.KRT1226Gjenopprettingsplaner_M";
            var bindings = LayoutToModel.GetDataModelBindings(layouts.Values);
            var generatedModel = LayoutToModel.Convert(bindings, modelName!);
            
            
            context.AddSource("model.g.cs", generatedModel);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required for this one
        }
    }
}