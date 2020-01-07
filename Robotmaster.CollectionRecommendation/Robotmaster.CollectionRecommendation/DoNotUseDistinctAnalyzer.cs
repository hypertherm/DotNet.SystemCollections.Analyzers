using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Robotmaster.CollectionRecommendation.Helpers;

namespace Robotmaster.CollectionRecommendation
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DoNotUseDistinctAnalyzer : DiagnosticAnalyzer
    {
        public static readonly  string DiagnosticId = AnalyzerHelper.GetCompleteAnalyzerId(3);
        internal static readonly LocalizableString Title = "Use a set-based collection instead of Distinct()";
        internal static readonly LocalizableString MessageFormat = "Instead of using the Distinct extension method, which is linear O(n), use a set-based collection (i.e. HashSet<T>) instead. They have a constant time (i.e. O(1)) lookups and additions are O(1) while the size doesn't exceeds the capacity, otherwise it's O(n).";
        internal const string Category = "Performance";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.SimpleMemberAccessExpression);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is MemberAccessExpressionSyntax memberAccessExpression && memberAccessExpression.Name.Identifier.ValueText.Equals("Distinct", StringComparison.Ordinal))
            {
                var methodSymbol = context.SemanticModel.GetSymbolInfo(memberAccessExpression).Symbol;
                if (methodSymbol.ContainingNamespace.Name.Equals("Linq", StringComparison.Ordinal))
                {
                    // For all such symbols, produce a diagnostic.
                    var diagnostic = Diagnostic.Create(Rule, memberAccessExpression.GetLocation(), memberAccessExpression.Name);

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
