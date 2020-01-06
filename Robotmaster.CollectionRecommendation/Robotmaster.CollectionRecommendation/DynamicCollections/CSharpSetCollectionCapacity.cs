using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Robotmaster.CollectionRecommendation.DynamicCollections
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CSharpSetCollectionCapacity : DiagnosticAnalyzer
    {
        public const string SetCapacitityRuleId = "RCR001";
        internal static readonly LocalizableString Title = "When known, please set the capacity of the dynamic collection";
        internal static readonly LocalizableString MessageFormat = "'{0}' is a dynamic collection (i.e. a List for instance) and not setting the capacity at the initialization phase of the collection is horrendous for performance when it can be set with {1}.";
        internal static readonly LocalizableString Description = "The default capacity of dynamic collection is initially set at 0 and becomes 4 at the first insertion. Over time, every time the collection count reaches its capacity limit, an extra allocation must be made and the total capacity is doubled. For big collections, insertions is a constant O(1) operation until it reaches the limited capacity and then it becomes O(n) where n is the total size of the collection.";
        internal const string Category = "Performance";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(SetCapacitityRuleId, Title, MessageFormat, Category, DiagnosticSeverity.Error, true, Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.MethodDeclaration);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is MethodDeclarationSyntax methodDeclaration)
            {
            }

        }
    }
}
