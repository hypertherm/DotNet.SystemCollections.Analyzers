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
    ///     This analyzer is used to monitor and detect when an ICollection calls the LINQ <see cref="Enumerable.Single{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> or <see cref="Enumerable.Single{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension methods.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DoNotUseUnnecessarySingleOnICollection : DiagnosticAnalyzer
    {
        /// <summary>
        ///     This is the complete ID of the rule for this analyzer.
        /// </summary>
        public static readonly string DiagnosticId = AnalyzerHelper.GetCompleteAnalyzerId(IdNumber);

        /// <summary>
        ///     The rule (i.e. <see cref="DiagnosticDescriptor"/>) covered by this analyzer.
        /// </summary>
#pragma warning disable RS1017 // DiagnosticId for analyzers must be a non-null constant.
        internal static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, AnalyzerHelper.AnalyzerTitle, MessageFormat, AnalyzerCategory.Collections, DiagnosticSeverity.Warning, true, Description);
#pragma warning restore RS1017 // DiagnosticId for analyzers must be a non-null constant.

        /// <summary>
        ///     This is the format of the analyzer's rule.
        /// </summary>
        private const string MessageFormat = "This ICollection is calling the Single()/SingleOrDefault() extension method; it should use the Count property instead";

        /// <summary>
        ///     This is the description of the analyzer's rule.
        /// </summary>
        private const string Description = "Use the Count property of the collection to validate that it's the only item instead of using the Enumerable.Single/Enumerable.SingleOrDefault extension method. Using the extension method requires moving twice inside the collection whereas through the use of a conditional, you can have a O(1) operation accessing the elemnt if present and the collection is of size 1.";

        /// <summary>
        ///     The number portion of the above <see cref="DiagnosticId"/>.
        /// </summary>
        private const int IdNumber = 009;

        /// <summary>
        ///     This is the name of the <see cref="Enumerable.Single{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        private const string SingleMethodName = nameof(Enumerable.Single);

        /// <summary>
        ///     This is the name of the <see cref="Enumerable.SingleOrDefault{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        private const string SingleOrDefaultMethodName = nameof(Enumerable.SingleOrDefault);

        /// <summary>
        ///     Gets the set of supported diagnostics covered by this analyzer.
        /// </summary>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <summary>
        ///     This is used to initialize the analyzer.
        /// </summary>
        /// <param name="context">
        ///     The context the analysis should take place in.
        /// </param>
        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            // If this corresponds to an ICollection invoking the Single()/SingleOrDefault() method.
            if (CollectionHelper.IsCollectionInvokingRedundantLinqMethod(context, SingleMethodName) || CollectionHelper.IsCollectionInvokingRedundantLinqMethod(context, SingleOrDefaultMethodName))
            {
                // Report a diagnostic for this invocations expression.
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
            }
        }
    }
}
