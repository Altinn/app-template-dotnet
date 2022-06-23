using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
namespace Altinn.App.ModelGenerator;

public static class JsonValidator
{
    public static List<Diagnostic> GetJsonParseDiagnostics(string filename, string content)
    {
        var ret = new List<Diagnostic>();
        try
        {
            var options = new JsonDocumentOptions()
            {
                CommentHandling = JsonCommentHandling.Skip, // stylecop.json includes comments
            };
            JsonNode.Parse(content, null,  options);
        }
        catch (JsonException e)
        {
            var linePosition = new LinePosition((int)e.LineNumber, (int)e.BytePositionInLine);
            ret.Add(Diagnostic.Create(AltinnDiagnosticsDescriptors.JsonParseError,
                        Location.Create(filename, new TextSpan(0, content.Length), new LinePositionSpan(linePosition, linePosition)),
                        e.Message));
        }
        return ret;
    }
}
