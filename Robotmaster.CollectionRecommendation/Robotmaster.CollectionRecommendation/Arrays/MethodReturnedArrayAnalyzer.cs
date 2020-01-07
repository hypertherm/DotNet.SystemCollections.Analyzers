using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Robotmaster.CollectionRecommendation.Arrays
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MethodReturnedArrayAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "RCR0001";
        private const string Title = "Collection Recommendations";
        internal const string MessageFormat = "The {0} method returned an Array instead of an IReadOnlyList.";
        private const string Description = "This is when an array is returned by a method instead of an IReadOnlyList.";
        private const string Category = "Arrays";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Method);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var methodSymbol = (IMethodSymbol)context.Symbol;

            if (!methodSymbol.ReturnsVoid && methodSymbol.ReturnType is IArrayTypeSymbol)
            {
                foreach (var location in methodSymbol.Locations)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, location, methodSymbol.Name));
                }
            }
        }
    }
}
