using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Robotmaster.CollectionRecommendation.Arrays
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class PropertyOfArrayAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "RCR0003";
        private const string Title = "Collection Recommendations";
        internal const string MessageFormat = "The {0} property has a type of an Array instead of an IReadOnlyList.";
        private const string Description = "This is when a property has a type of an Array instead of an IReadOnlyList.";
        private const string Category = "Arrays";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true, Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Property);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            IPropertySymbol propertySymbol = (IPropertySymbol)context.Symbol;

            if (propertySymbol.Type is IArrayTypeSymbol arrayTypeSymbol)
            {
                foreach (var location in propertySymbol.Locations)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, location, propertySymbol.Name));
                }
            }
        }
    }
}
