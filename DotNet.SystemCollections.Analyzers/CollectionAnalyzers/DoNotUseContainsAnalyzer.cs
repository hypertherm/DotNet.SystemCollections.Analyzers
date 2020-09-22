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
    ///     This analyzer is used to monitor and detect when an IEnumerable calls the LINQ <see cref="Enumerable.Contains{TSource}(System.Collections.Generic.IEnumerable{TSource},TSource)"/> extension method.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DoNotUseContainsAnalyzer : DiagnosticAnalyzer
    {
        /// <summary>
        ///     This is the complete ID of the rule for this analyzer.
        /// </summary>
        public static readonly string DiagnosticId = AnalyzerHelper.GetCompleteAnalyzerId(IdNumber);

        /// <summary>
        ///     This is the rule (i.e. <see cref="DiagnosticDescriptor"/>) handled by this analyzer.
        /// </summary>
#pragma warning disable RS1017 // DiagnosticId for analyzers must be a non-null constant.
        internal static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, AnalyzerHelper.AnalyzerTitle, MessageFormat, AnalyzerCategory.Collections, DiagnosticSeverity.Warning, true, Description);
#pragma warning restore RS1017 // DiagnosticId for analyzers must be a non-null constant.

        /// <summary>
        ///     This is the format of the analyzer's rule.
        /// </summary>
        private const string MessageFormat = "This IEnumerable is calling the Contains() extension method; the developer should either use a Dictionary or a Set collection to make the operation O(1) instead of O(n).";

        /// <summary>
        ///     This is the description of the analyzer's rule.
        /// </summary>
        private const string Description = "All IEnumerable expressions that require to determine the presence of a specific value should leave the work to a specialized collection like Dictionary<TKey, TValue> or even HashSet<T> instead of using the Enumerable.Contains() extension method.";

        /// <summary>
        ///     The number portion of the above <see cref="DiagnosticId"/>.
        /// </summary>
        private const int IdNumber = 002;

        /// <summary>
        ///     This is the name of the <see cref="Enumerable.Contains{TSource}(System.Collections.Generic.IEnumerable{TSource},TSource)"/> extension method.
        /// </summary>
        private const string ContainsMethodName = nameof(Enumerable.Contains);

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
            // If this corresponds to an IList invoking the Contains() method.
            if (CollectionHelper.IsIEnumerableInvokingRedundantLinqMethod(context, ContainsMethodName))
            {
                // Report a diagnostic for this invocations expression.
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
            }
        }
    }
}
