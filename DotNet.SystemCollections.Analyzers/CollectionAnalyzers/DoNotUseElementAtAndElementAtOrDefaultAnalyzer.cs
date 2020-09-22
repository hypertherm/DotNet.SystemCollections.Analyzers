namespace DotNet.SystemCollections.Analyzers.Collections
{
    using System.Collections.Immutable;
    using System.Linq;
    using DotNet.SystemCollections.Analyzers.Helpers;
    using DotNet.SystemCollections.Analyzers.Helpers.Collections;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    ///     This analyzer is used to monitor and detect when an IList calls the LINQ <see cref="Enumerable.ElementAt{TSource}"/> or <see cref="Enumerable.ElementAtOrDefault{TSource}"/> extension method.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DoNotUseElementAtAndElementAtOrDefaultAnalyzer : DiagnosticAnalyzer
    {
        /// <summary>
        ///     This is the complete ID of the rule for this analyzer.
        /// </summary>
        public static readonly string DiagnosticId = AnalyzerHelper.GetCompleteAnalyzerId(IdNumber);

        /// <summary>
        ///     The rule (i.e. <see cref="DiagnosticDescriptor"/>) handled by this analyzer.
        /// </summary>
#pragma warning disable RS1017 // DiagnosticId for analyzers must be a non-null constant.
        internal static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, AnalyzerHelper.AnalyzerTitle, MessageFormat, AnalyzerCategory.Collections, DiagnosticSeverity.Warning, true, Description);
#pragma warning restore RS1017 // DiagnosticId for analyzers must be a non-null constant.

        /// <summary>
        ///     This is the format of the analyzer's rule.
        /// </summary>
        private const string MessageFormat = "This IList is calling the ElementAt() or ElementAtOrDefault() extension method; it should use indexing instead.";

        /// <summary>
        ///     This is the description of the analyzer's rule.
        /// </summary>
        private const string Description = "All IList collections can access any item by using the Item indexer property which is a O(1) operation instead using of the Enumerable.ElementAt() or Enumerable.ElementAt() extension method.";

        /// <summary>
        ///     The number portion of the above <see cref="DiagnosticId"/>.
        /// </summary>
        private const int IdNumber = 005;

        /// <summary>
        ///     This is the name of the <see cref="Enumerable.ElementAt{TSource}"/> extension method.
        /// </summary>
        private const string ElementAtMethodName = nameof(Enumerable.ElementAt);

        /// <summary>
        ///     This is the name of the <see cref="Enumerable.ElementAtOrDefault{TSource}"/> extension method.
        /// </summary>
        private const string ElementAtOrDefaultMethodName = nameof(Enumerable.ElementAtOrDefault);

        /// <summary>
        ///     Gets the set of rules handled by this analyzer.
        /// </summary>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <summary>
        ///     This is used to initialize the analyzer.
        /// </summary>
        /// <param name="context">
        ///     The context in which the analysis takes place.
        /// </param>
        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            // If this corresponds to an IList invoking the LongCount() method.
            if (CollectionHelper.IsListInvokingRedundantLinqMethod(context, ElementAtMethodName) || CollectionHelper.IsListInvokingRedundantLinqMethod(context, ElementAtOrDefaultMethodName))
            {
                // Report a diagnostic for this invocations expression.
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
            }
        }
    }
}
