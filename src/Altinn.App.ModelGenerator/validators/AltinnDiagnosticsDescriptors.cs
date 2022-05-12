using Microsoft.CodeAnalysis;

namespace Altinn.App.ModelGenerator;

public static class AltinnDiagnosticsDescriptors
{
    public static readonly DiagnosticDescriptor JsonParseError
        = new DiagnosticDescriptor(
                id: "ALT001",
                title: "Error in json parsing",
                messageFormat: "{0}",
                category: "AltinnAnalyzer",
                defaultSeverity: DiagnosticSeverity.Error,
                isEnabledByDefault: true );
}
