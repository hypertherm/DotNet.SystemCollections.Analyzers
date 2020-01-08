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
using Robotmaster.CollectionRecommendation.Helpers.Lists;

namespace Robotmaster.CollectionRecommendation
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DoNotUseDistinctAnalyzer : DiagnosticAnalyzer
    {
        /// <summary>
        ///     This is the complete ID of the rule for this analyzer.
        /// </summary>
        public static readonly  string DiagnosticId = AnalyzerHelper.GetCompleteAnalyzerId(IdNumber);

        /// <summary>
        ///     This is the format of the analyzer's rule.
        /// </summary>
        internal static readonly LocalizableString Title = "Use a set-based collection instead of Distinct()";

        /// <summary>
        ///     This is the message of the analyzer's rule.
        /// </summary>
        internal static readonly LocalizableString MessageFormat = "Instead of using the Distinct extension method, which is linear O(n), use a set-based collection (i.e. HashSet<T>) instead. They have a constant time (i.e. O(1)) lookups and additions are O(1) while the size doesn't exceeds the capacity, otherwise it's O(n).";

        /// <summary>
        ///     The category of the analyzer's rule.
        /// </summary>
        private const string Category = "Collections";

        /// <summary>
        ///     The number portion of the above <see cref="DiagnosticId"/>.
        /// </summary>
        private const int IdNumber = 7;

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, true);

        /// <summary>
        ///     This is the name of the <see cref="Enumerable.LongCount{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        private static readonly string DistinctMethodName = nameof(Enumerable.Distinct);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.SimpleMemberAccessExpression);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            // If this corresponds to an IList invoking the Distinct() method.
            if (CollectionHelper.IsICollectionInvokingRedundantLinqMethod(context, DistinctMethodName))
            {
                // Report a diagnostic for this invocations expression.
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
            }
        }
    }
}
