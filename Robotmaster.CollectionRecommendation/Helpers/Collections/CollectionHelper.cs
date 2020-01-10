﻿namespace Robotmaster.CollectionRecommendation.Helpers.Collections
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
        ///     This is the fully qualified namespace for old-style collections (residing in System.Collections).
        /// </summary>
        private static readonly string OldStyleCollectionNamespace = typeof(ICollection).Namespace;

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
        internal static bool IsListInvokingRedundantLinqMethod(SyntaxNodeAnalysisContext context, string linqMethodName) => IsTypeInvokingRedundantLinqMethod(context, linqMethodName, IsIList);

        /// <summary>
        ///     This is used to determine if a non-IList is invoking the specified no-parameter overload of a LINQ method in <see cref="Enumerable"/> called <paramref name="linqMethodName"/>.
        /// </summary>
        /// <param name="context">
        ///     The <see cref="SymbolAnalysisContext"/> to use.
        /// </param>
        /// <param name="linqMethodName">
        ///     The name of the no-parameter overload of a LINQ method in <see cref="Enumerable"/>.
        /// </param>
        /// <returns>
        ///     This returns whether or not this corresponds to an non-IList invoking a LINQ method.
        /// </returns>
        internal static bool IsNonListInvokingRedundantLinqMethod(SyntaxNodeAnalysisContext context, string linqMethodName) => IsTypeInvokingRedundantLinqMethod(context, linqMethodName, IsNotIList);

        /// <summary>
        ///     This is used to determine if a ICollection is invoking the specified no-parameter overload of a LINQ method in <see cref="Enumerable"/> called <paramref name="linqMethodName"/>.
        /// </summary>
        /// <param name="context">
        ///     The <see cref="SymbolAnalysisContext"/> to use.
        /// </param>
        /// <param name="linqMethodName">
        ///     The name of the no-parameter overload of a LINQ method in <see cref="Enumerable"/>.
        /// </param>
        /// <returns>
        ///     This returns whether or not this corresponds to an ICollection invoking a LINQ method.
        /// </returns>
        internal static bool IsCollectionInvokingRedundantLinqMethod(SyntaxNodeAnalysisContext context, string linqMethodName) => IsTypeInvokingRedundantLinqMethod(context, linqMethodName, IsICollection);

        /// <summary>
        ///     This is used to determine if the given <paramref name="iNamedTypeSymbol"/> corresponds to an old-style collection type 
        /// </summary>
        /// <param name="iNamedTypeSymbol"></param>
        /// <returns></returns>
        internal static bool IsOldStyleCollectionClass(INamedTypeSymbol iNamedTypeSymbol) => iNamedTypeSymbol.TypeKind == TypeKind.Class && string.Equals(iNamedTypeSymbol.ContainingNamespace.GetFullNameWithoutPrefix(), OldStyleCollectionNamespace, StringComparison.Ordinal);

        /// <summary>
        ///     This is used to determine if the given <paramref name="iNamedTypeSymbol"/> corresponds to the <see cref="ICollection"/> interface type.
        /// </summary>
        /// <param name="iNamedTypeSymbol">
        ///     The <see cref="INamedTypeSymbol"/> to check.
        /// </param>
        /// <returns>
        ///     This returns a <see cref="bool"/> indicating whether or not the given collection corresponds to the <see cref="ICollection"/> interface type.
        /// </returns>
        private static bool IsICollection(INamedTypeSymbol iNamedTypeSymbol) => HasExpectedInterface(iNamedTypeSymbol, CollectionInterfaceFullType);

        /// <summary>
        ///     This is used to determine if the given <paramref name="iNamedTypeSymbol"/> corresponds to the <see cref="IList{T}" /> interface type.
        /// </summary>
        /// <param name="iNamedTypeSymbol">
        ///     The named type.
        /// </param>
        /// <returns>
        ///     Whether or not there was a match on the <see cref="IList{T}" /> interface type.
        /// </returns>
        private static bool IsIList(INamedTypeSymbol iNamedTypeSymbol) => HasExpectedInterface(iNamedTypeSymbol, ListInterfaceFullType);

        /// <summary>
        ///     This is used to determine if the given <paramref name="iNamedTypeSymbol"/> does not correspond to the <see cref="IList{T}" /> interface type.
        /// </summary>
        /// <param name="iNamedTypeSymbol">
        ///     The named type.
        /// </param>
        /// <returns>
        ///     Whether or not there was not a match on the <see cref="IList{T}" /> interface type.
        /// </returns>
        private static bool IsNotIList(INamedTypeSymbol iNamedTypeSymbol) => !IsIList(iNamedTypeSymbol);

        private static bool IsTypeInvokingRedundantLinqMethod(SyntaxNodeAnalysisContext context, string linqMethodName, Func<INamedTypeSymbol, bool> typeMatchFunc)
        {
            // If the node is not an InvocationExpression.
            if (!(context.Node is InvocationExpressionSyntax syntaxNode))
            {
                // It is not an invocation expression; return false.
                return false;
            }

            // Get the information for the method.
            ISymbol symbol = context.SemanticModel.GetSymbolInfo(syntaxNode).Symbol;

            // If it is not an extension method.
            if (!(symbol is IMethodSymbol methodSymbol) || !methodSymbol.IsExtensionMethod)
            {
                // It cannot be the correct method; return false.
                return false;
            }

            // If the name of the method does not match the one parameter overload of the Count() method.
            if (!string.Equals(methodSymbol.Name, linqMethodName, StringComparison.Ordinal) || methodSymbol.Parameters.Length != 0)
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
            if (!string.Equals(fullDisplayString, CollectionHelper.EnumerableClassFullName, StringComparison.Ordinal))
            {
                // This is not the correct Count() method; return false.
                return false;
            }

            // The method being called is the exact method that needs to be analyzed.

            // If the expression is not a MemberAccessExpressionSyntax.
            if (!(syntaxNode.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax))
            {
                // The expression is not in the correct form; return false.
                return false;
            }

            // Get the type of the expression.
            ITypeSymbol expressionTypeSymbol = context.SemanticModel.GetTypeInfo(memberAccessExpressionSyntax.Expression).Type;

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
                    // Return whether or not the type matches.
                    return typeMatchFunc.Invoke(namedTypeSymbol);
                }

                default:
                {
                    // The type of the expression is not a collection.
                    return false;
                }
            }
        }

        private static bool HasExpectedInterface(INamedTypeSymbol iNamedTypeSymbol, string expectedInterfaceFullType)
        {
            // Go through all of the expression named type's interfaces.
            foreach (var interfaceNamedTypeSymbol in iNamedTypeSymbol.AllInterfaces)
            {
                // If the full name of this interface matches that of the expected interface.
                if (string.Equals(iNamedTypeSymbol.GetFullNameWithoutPrefix(), expectedInterfaceFullType, StringComparison.Ordinal))
                {
                    // Return true;
                    return true;
                }
            }

            // It is not an IList; return false.
            return false;
        }

        public static bool IsConcreteListType(SyntaxNodeAnalysisContext context)
        {
            // Get the information for the parameter.
            if (context.SemanticModel.GetSymbolInfo(context.Node).Symbol is IParameterSymbol parameterSymbol)
            {
                return true;
            }

            return false;
        }
    }
}