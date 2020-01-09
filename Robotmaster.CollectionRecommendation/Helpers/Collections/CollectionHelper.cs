using System.Collections.Generic;

namespace Robotmaster.CollectionRecommendation.Helpers.Collections
{
    using System;
    using System.Collections;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal class CollectionHelper
    {
        /// <summary>
        ///     This is the full name of the <see cref="Enumerable"/> class
        /// </summary>
        internal static readonly string EnumerableClassFullName = typeof(Enumerable).FullName;

        /// <summary>
        ///     This is the full name of the <see cref="ICollection"/> interface.
        /// </summary>
        private static readonly string CollectionInterfaceFullType = typeof(ICollection).FullName;

        /// <summary>
        ///     This is the full name of the <see cref="IList"/> interface.
        /// </summary>
        private static readonly string ListInterfaceFullType = typeof(IList).FullName;

        /// <summary>
        ///     This is the full name of the <see cref="IEnumerable"/> interface.
        /// </summary>
        private static readonly string EnumerableInterfaceFullType = typeof(IEnumerable).FullName;


        private static readonly HashSet<string> MethodRequiringSpecificChecks = new HashSet<string> { "Contains" };



        /// <summary>
        ///     This is used to determine if a IList is invoking the specified no-parameter overload of a LINQ method in <see cref="Enumerable"/> called <paramref name="linqMethodName"/>.
        /// </summary>
        /// <param name="context">
        ///     The <see cref="SymbolAnalysisContext"/> to use.
        /// </param>
        /// <param name="linqMethodName">
        ///     The name of the no-parameter overload of a LINQ method in <see cref="Enumerable"/>.
        /// </param>
        /// <returns>
        ///     This returns whether or not this corresponds to an IList invoking a LINQ method.
        /// </returns>
        internal static bool IsCollectionInvokingRedundantLinqMethod(SyntaxNodeAnalysisContext context, string linqMethodName, bool isExpectedToBeAnExtensionMethod = false)
        {
            switch (context.Node)
            {
                case InvocationExpressionSyntax syntaxNode when ShouldReportMisuseOfLinqApi(syntaxNode, context, linqMethodName):
                    return true;
                case MemberAccessExpressionSyntax syntaxNode when ShouldReportMisuseOfLinqApi(syntaxNode, context, linqMethodName):
                    return true;
                default:
                    return false;
            }
        }

        private static bool ShouldReportMisuseOfLinqApi(SyntaxNode syntaxNode, SyntaxNodeAnalysisContext context, string linqMethodName)
        {
            bool isInvocationExpression = syntaxNode is InvocationExpressionSyntax;
            bool isMemberAccessExpression = syntaxNode is MemberAccessExpressionSyntax;

            if (!(context.SemanticModel.GetSymbolInfo(syntaxNode).Symbol is IMethodSymbol methodSymbol))
            {
                // It cannot be the correct method; return false.
                return false;
            }

            // If the name of the method does not match the one parameter overload of the Count() method.
            if (!string.Equals(methodSymbol.Name, linqMethodName, StringComparison.Ordinal) && (!MethodRequiringSpecificChecks.Contains(methodSymbol.Name) || methodSymbol.Parameters.Length != 0))
            {
                // This is not the correct method; return false.
                return false;
            }

            // If the containing Symbol is not a NamedType.
            if (!(methodSymbol.ContainingSymbol is INamedTypeSymbol containingNamedTypeSymbol))
            {
                // The method is not contained in a NamedTypeSymbol; return false.
                return false;
            }

            // Get the full name of the method's containing type.
            string fullDisplayString = containingNamedTypeSymbol.GetFullNameWithoutPrefix();

            // If the Count() method does not belong to the Enumerable class.
            if (!string.Equals(fullDisplayString, EnumerableClassFullName, StringComparison.Ordinal) && !MethodRequiringSpecificChecks.Contains(methodSymbol.Name))
            {
                // This is not the correct Count() method; return false.
                return false;
            }

            ITypeSymbol expressionTypeSymbol = null;

            if (isInvocationExpression && ((InvocationExpressionSyntax)syntaxNode).Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
            {
                expressionTypeSymbol = context.SemanticModel.GetTypeInfo(memberAccessExpressionSyntax.Expression).Type;
            }

            if (isMemberAccessExpression)
            {
                expressionTypeSymbol = context.SemanticModel.GetTypeInfo(((MemberAccessExpressionSyntax)syntaxNode).Expression).Type;
            }

            if (expressionTypeSymbol == null)
            {
                // The expression is not in the correct form; return false.
                return false;
            }

            // Switch on the type of the expression.
            switch (expressionTypeSymbol)
            {
                // If it is an array type.
                case IArrayTypeSymbol _:
                    {
                        // An array is a collection; return true.
                        return true;
                    }

                // If it is a INamedTypeSymbol.
                case INamedTypeSymbol namedTypeSymbol:
                    {
                        // Go through all of the expression named type's interfaces.
                        foreach (var interfaceNamedTypeSymbol in namedTypeSymbol.AllInterfaces)
                        {
                            // Going from the more to less specific high level interface.

                            // If the collection is an IList<T>
                            if (DoesSymbolMatchOnCollectionTypeFullName(interfaceNamedTypeSymbol, ListInterfaceFullType))
                            {
                                // A match was found; return true.
                                return true;
                            }

                            // If the collection is an ICollection<T>
                            if (DoesSymbolMatchOnCollectionTypeFullName(interfaceNamedTypeSymbol, CollectionInterfaceFullType))
                            {
                                // A match was found; return true.
                                return true;
                            }

                            // If the collection is an IEnumerable<T>
                            if (DoesSymbolMatchOnCollectionTypeFullName(interfaceNamedTypeSymbol, EnumerableInterfaceFullType))
                            {
                                // A match was found; return true.
                                return true;
                            }
                        }

                        // This is used to determine if the given INamedTypedSymbol is the high level interface we're interested in.
                        bool DoesSymbolMatchOnCollectionTypeFullName(INamedTypeSymbol iNamedTypeSymbol, string collectionInterfaceFullTypeName) => string.Equals(iNamedTypeSymbol.GetFullNameWithoutPrefix(), collectionInterfaceFullTypeName, StringComparison.Ordinal);

                        // No match was found; just return false.
                        return false;
                    }

                default:
                    {
                        // The type of the expression is not a collection.
                        return false;
                    }
            }
        }
    }
}