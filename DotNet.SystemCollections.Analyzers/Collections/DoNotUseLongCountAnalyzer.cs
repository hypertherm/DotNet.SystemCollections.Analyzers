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
    ///     This analyzer is used to monitor and detect when an ICollection calls the LINQ <see cref="Enumerable.LongCount{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DoNotUseLongCountAnalyzer : DiagnosticAnalyzer
    {
        /// <summary>
        ///     This is the complete ID of the rule for this analyzer.
        /// </summary>
        public static readonly string DiagnosticId = AnalyzerHelper.GetCompleteAnalyzerId(IdNumber);

        /// <summary>
        ///     This is the format of the analyzer's rule.
        /// </summary>
        internal const string MessageFormat = "This ICollection is calling the LongCount() extension method; it should use the Count property instead.";

        /// <summary>
        ///     This is the description of the analyzer's rule.
        /// </summary>
        private const string Description = "All ICollections should use the Count property instead using of the Enumerable.LongCount() extension method.";

        /// <summary>
        ///     The category of the analyzer's rule.
        /// </summary>
        private const string Category = "Collections";

        /// <summary>
        ///     The number portion of the above <see cref="DiagnosticId"/>.
        /// </summary>
        private const int IdNumber = 008;

        /// <summary>
        ///     This is the name of the <see cref="Enumerable.LongCount{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        private const string LongCountMethodName = nameof(Enumerable.LongCount);

#pragma warning disable RS1017 // DiagnosticId for analyzers must be a non-null constant.
        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, AnalyzerHelper.AnalyzerTitle, MessageFormat, Category, DiagnosticSeverity.Warning, true, Description);
#pragma warning restore RS1017 // DiagnosticId for analyzers must be a non-null constant.

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            // If this corresponds to an ICollection invoking the LongCount() method.
            if (CollectionHelper.IsCollectionInvokingRedundantLinqMethod(context, LongCountMethodName))
            {
                // Report a diagnostic for this invocations expression.
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
            }
        }
    }
}
