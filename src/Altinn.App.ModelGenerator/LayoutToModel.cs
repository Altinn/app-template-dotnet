using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
namespace Altinn.App.ModelGenerator
{
    public class LayoutToModel
    {
        public static string Convert(IEnumerable<string> bindings, string fullClassName)
        {
            var (ns, className) = SplitFullClassname(fullClassName);
            var three = new ThreeRep(className);
            foreach (var binding in bindings)
            {
                three.AddDottedPath(binding);
            }
            return $"// Auto-generated\n#nullable enable\nnamespace {ns}\n{{\n\n{three.ToPoco()}\n}}\n";
        }

        public static (string, string) SplitFullClassname(string fullClassName)
        {
            var index = fullClassName.LastIndexOf('.');
            return (fullClassName.Substring(0,index), fullClassName.Substring(index+1));
        }

        public static List<string> GetDataModelBindings(IEnumerable<string> layouts)
        {
            var ret = new List<string>();
            foreach (var layout in layouts)
            {
                var node = JsonNode.Parse(layout)?["data"]?["layout"] as JsonArray;
                if (node != null)
                {
                    foreach (var component in node)
                    {
                        var bindings = component?["dataModelBindings"] as JsonObject;
                        if(bindings != null)
                        {
                            foreach(var binding in bindings)
                            {
                                if((binding.Value as JsonValue)?.TryGetValue<string>(out string? value) ?? false)
                                {
                                    ret.Add(value!);
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
}