namespace DotNet.SystemCollections.Analyzers.Helpers.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal class CollectionHelper
    {
        /// <summary>
        ///     The reserved prefix used for the getter methods of properties.
        /// </summary>
        internal const string PropertyGetterPrefix = "get_";

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
        internal static bool IsIEnumerableInvokingRedundantLinqMethod(SyntaxNodeAnalysisContext context, string linqMethodName) => IsTypeInvokingRedundantLinqMethod(context, linqMethodName, IsIEnumerable);

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
        ///     This is used to determine if a non-Array is invoking the specified no-parameter overload of a LINQ method in <see cref="Enumerable"/> called <paramref name="linqMethodName"/>.
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
        internal static bool IsNonArrayInvokingRedundantLinqMethod(SyntaxNodeAnalysisContext context, string linqMethodName) => IsTypeInvokingRedundantLinqMethod(context, linqMethodName, IsNotArray);

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
        ///     This is used to determine if the given <paramref name="iNamedTypeSymbol"/> corresponds to an old-style collection type.
        /// </summary>
        /// <param name="iNamedTypeSymbol">
        ///     The named type.
        /// </param>
        /// <returns>
        ///     
        /// </returns>
        internal static bool IsOldStyleCollectionClass(INamedTypeSymbol iNamedTypeSymbol) => iNamedTypeSymbol.TypeKind == TypeKind.Class && string.Equals(iNamedTypeSymbol.ContainingNamespace.GetFullNameWithoutPrefix(), OldStyleCollectionNamespace, StringComparison.Ordinal);

        /// <summary>
        ///     This is used to determine if the given <paramref name="iTypeSymbol"/> corresponds to the <see cref="ICollection"/> interface type.
        /// </summary>
        /// <param name="iTypeSymbol">
        ///     The <see cref="ITypeSymbol"/> to check.
        /// </param>
        /// <returns>
        ///     This returns a <see cref="bool"/> indicating whether or not the given collection corresponds to the <see cref="ICollection"/> interface type.
        /// </returns>
        private static bool IsICollection(ITypeSymbol iTypeSymbol)
        {
            // Switch on the type of the expression.
            switch (iTypeSymbol)
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
                    // Return whether or not the type is a collection.
                    return HasExpectedInterface(namedTypeSymbol, CollectionInterfaceFullType);
                }

                default:
                {
                    // The type of the expression is not a collection.
                    return false;
                }
            }
        }

        /// <summary>
        ///     This is used to determine if the given <paramref name="iTypeSymbol"/> corresponds to the <see cref="IList{T}" /> interface type.
        /// </summary>
        /// <param name="iTypeSymbol">
        ///     The type.
        /// </param>
        /// <returns>
        ///     Whether or not there was a match on the <see cref="IList{T}" /> interface type.
        /// </returns>
        private static bool IsIList(ITypeSymbol iTypeSymbol) => (iTypeSymbol is INamedTypeSymbol iNamedTypeSymbol) && HasExpectedInterface(iNamedTypeSymbol, ListInterfaceFullType);

        /// <summary>
        ///     This is used to determine if the given <paramref name="iTypeSymbol"/> corresponds to the <see cref="IEnumerable{T}" /> interface type.
        /// </summary>
        /// <param name="iTypeSymbol">
        ///     The type.
        /// </param>
        /// <returns>
        ///     Whether or not there was a match on the <see cref="IEnumerable{T}" /> interface type.
        /// </returns>
        private static bool IsIEnumerable(ITypeSymbol iTypeSymbol) => (iTypeSymbol is INamedTypeSymbol iNamedTypeSymbol) && HasExpectedInterface(iNamedTypeSymbol, EnumerableInterfaceFullType);

        /// <summary>
        ///     This is used to determine if the given <paramref name="iNamedTypeSymbol"/> does not correspond to the <see cref="IList{T}" /> interface type.
        /// </summary>
        /// <param name="iTypeSymbol">
        ///     The type.
        /// </param>
        /// <returns>
        ///     Whether or not there was not a match on the <see cref="IList{T}" /> interface type.
        /// </returns>
        private static bool IsNotIList(ITypeSymbol iTypeSymbol) => !IsIList(iTypeSymbol);

        /// <summary>
        ///     This is used to determine if the given <paramref name="iTypeSymbol"/> corresponds to an array type.
        /// </summary>
        /// <param name="iTypeSymbol">
        ///     The type.
        /// </param>
        /// <returns>
        ///     Whether or not the given <paramref name="iTypeSymbol"/> corresponds to an array type.
        /// </returns>
        private static bool IsArray(ITypeSymbol iTypeSymbol) => iTypeSymbol is IArrayTypeSymbol;

        /// <summary>
        ///     This is used to determine if the given <paramref name="iTypeSymbol"/> does not correspond to an array type.
        /// </summary>
        /// <param name="iTypeSymbol">
        ///     The type.
        /// </param>
        /// <returns>
        ///     Whether or not there was not a match with an array type.
        /// </returns>
        private static bool IsNotArray(ITypeSymbol iTypeSymbol) => !IsArray(iTypeSymbol);

        private static bool IsTypeInvokingRedundantLinqMethod(SyntaxNodeAnalysisContext context, string linqMethodName, Func<ITypeSymbol, bool> typeMatchFunc)
        {
            switch (context.Node)
            {
                case InvocationExpressionSyntax syntaxNode when ShouldReportMisuseOfLinqApi(syntaxNode, context, linqMethodName, typeMatchFunc):
                    return true;
                case MemberAccessExpressionSyntax syntaxNode when ShouldReportMisuseOfLinqApi(syntaxNode, context, linqMethodName, typeMatchFunc):
                    return true;
                default:
                    return false;
            }
        }

        private static bool ShouldReportMisuseOfLinqApi(SyntaxNode syntaxNode, SyntaxNodeAnalysisContext context, string linqMethodName, Func<ITypeSymbol, bool> typeMatchFunc)
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

            // Call the type matching function and return its value.
            return typeMatchFunc.Invoke(expressionTypeSymbol);
        }

        private static bool HasExpectedInterface(INamedTypeSymbol iNamedTypeSymbol, string expectedInterfaceFullType)
        {
            // Go through all of the expression named type's interfaces.
            foreach (var interfaceNamedTypeSymbol in iNamedTypeSymbol.AllInterfaces)
            {
                // If the full name of this interface matches that of the expected interface.
                if (string.Equals(interfaceNamedTypeSymbol.GetFullNameWithoutPrefix(), expectedInterfaceFullType, StringComparison.Ordinal))
                {
                    // Return true;
                    return true;
                }
            }

            // It is not an IList; return false.
            return false;
        }
    }
}
