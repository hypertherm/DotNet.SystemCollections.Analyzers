using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Robotmaster.CollectionRecommendation.Helpers;
using Robotmaster.CollectionRecommendation.Helpers.Collections;

namespace Robotmaster.CollectionRecommendation.Collections
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class DoNotUseUnnecessarySingleOnICollection : DiagnosticAnalyzer
    {
        /// <summary>
        ///     This is the complete ID of the rule for this analyzer.
        /// </summary>
        public static readonly string DiagnosticId = AnalyzerHelper.GetCompleteAnalyzerId(IdNumber);

        /// <summary>
        ///     This is the format of the analyzer's rule.
        /// </summary>
        internal const string MessageFormat = "This ICollection is calling the Single()/SingleOrDefault() extension method; it should use the Count property instead.";

        /// <summary>
        ///     This is the description of the analyzer's rule.
        /// </summary>
        private const string Description = "Use the Count property of the collection to validate that it's the only item instead of using the Enumerable.Single/Enumerable.SingleOrDefault extension method.";

        /// <summary>
        ///     The category of the analyzer's rule.
        /// </summary>
        private const string Category = "Collections";

        /// <summary>
        ///     The number portion of the above <see cref="DiagnosticId"/>.
        /// </summary>
        private const int IdNumber = 8;

#pragma warning disable RS1017 // DiagnosticId for analyzers must be a non-null constant.
        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, AnalyzerHelper.AnalyzerTitle, MessageFormat, Category, DiagnosticSeverity.Warning, true, Description);
#pragma warning restore RS1017 // DiagnosticId for analyzers must be a non-null constant.

        /// <summary>
        ///     This is the name of the <see cref="Enumerable.Single{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        private static readonly string SingleMethodName = nameof(Enumerable.Single);

        /// <summary>
        ///     This is the name of the <see cref="Enumerable.SingleOrDefault{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        private static readonly string SingleOrDefaultMethodName = nameof(Enumerable.SingleOrDefault);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            // If this corresponds to an IList invoking the LongCount() method.
            if (CollectionHelper.IsCollectionInvokingRedundantLinqMethod(context, SingleMethodName) || CollectionHelper.IsCollectionInvokingRedundantLinqMethod(context, SingleOrDefaultMethodName) && !IsSingleInvokedLazySequence())
            {
                // Report a diagnostic for this invocations expression.
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
            }

            bool IsSingleInvokedLazySequence()
            {
                // Get the information for the method.
                ISymbol symbol = context.SemanticModel.GetSymbolInfo(context.Node).Symbol;

                return false;
            }
        }
    }
}
