using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Robotmaster.CollectionRecommendation.Arrays
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class FieldOfArrayAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "RCR0002";
        private const string Title = "Collection Recommendations";
        internal const string MessageFormat = "The {0} field has a type of an Array instead of an IReadOnlyList.";
        private const string Description = "This is when a field has a type of an Array instead of an IReadOnlyList.";
        private const string Category = "Arrays";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true, Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Field);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            IFieldSymbol fieldSymbol = (IFieldSymbol)context.Symbol;

            if (fieldSymbol.Type is IArrayTypeSymbol arrayTypeSymbol)
            {
                foreach (var location in fieldSymbol.Locations)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, location, fieldSymbol.Name));
                }
            }
        }
    }
}
