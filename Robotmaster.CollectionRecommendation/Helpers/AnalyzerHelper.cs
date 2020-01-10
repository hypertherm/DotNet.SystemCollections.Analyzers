// <copyright file="AnalyzerHelper.cs" company="Hypertherm Robotic Software Inc.">
// Copyright (c) Hypertherm Robotic Software Inc. All rights reserved.
// </copyright>

using Microsoft.CodeAnalysis;

namespace Robotmaster.CollectionRecommendation.Helpers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     This is a set of common helper methods, useful to all PDK analyzers.
    /// </summary>
    internal static class AnalyzerHelper
    {
        /// <summary>
        ///     This is the title for all of the PDK analyzers.
        /// </summary>
        internal const string AnalyzerTitle = "Collection Recommendations";

        /// <summary>
        ///     This is the common prefix for the complete analyzer ID, used to identify all of the RCR-related analyzer rules.
        /// </summary>
        private const string AnalyzerIdPrefix = "RCR";

        /// <summary>
        ///     This is the prefix represents the Global prefix for full (i.e. fully qualified) names of types.
        /// </summary>
        private const string GlobalFullNamePrefix = "global::";

        /// <summary>
        ///     This is the minimum number of digits in the ID number portion of the analyzer ID; if less than the specified number is provided, zero padding will occur.
        /// </summary>
        private const int AnalyzerIdMinimumNumberOfDigits = 4;

        /// <summary>
        ///     This is used to ensure uniqueness amongst analyzer IDs.
        /// </summary>
        private static readonly HashSet<int> AnalyzerIds = new HashSet<int>();

        /// <summary>
        ///     This is used to return the complete analyzer ID, given the <paramref name="idNumber"/>.
        /// </summary>
        /// <param name="idNumber">
        ///     The ID number of the specific PDK analyzer.
        /// </param>
        /// <returns>
        ///     This returns the complete ID for the PDK analyzer with the given <paramref name="idNumber"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     This will be thrown if:
        ///         1) The given <paramref name="idNumber"/> is less than or equal to zero.
        ///         2) The given <paramref name="idNumber"/> has already been used.
        /// </exception>
        internal static string GetCompleteAnalyzerId(int idNumber)
        {
            // If the ID number is less than or equal to zero.
            if (idNumber < 0)
            {
                // All of the IDs must be positive; throw an exception.
                throw new ArgumentOutOfRangeException($"The given analyzer ID was {idNumber} but all analyzer IDs must be positive!");
            }

            // If it is not possible to add this ID to the list of analyzer IDs (because it has already been used.)
            if (!AnalyzerIds.Add(idNumber))
            {
                // All of the IDs must be unique; throw an exception.
                throw new ArgumentOutOfRangeException($"The given ID {idNumber} is used by at least one other analyzer!");
            }

            // The ID is valid.

            // Return the complete analyzer ID string.
            return AnalyzerIdPrefix + idNumber.ToString($"D{AnalyzerIdMinimumNumberOfDigits}");
        }

        /// <summary>
        ///     This is used to attempt to get the <see cref="INamedTypeSymbol"/> from the given <paramref name="iTypeSymbol"/>.
        /// </summary>
        /// <param name="iTypeSymbol">
        ///     The given <see cref="ITypeSymbol"/>.
        /// </param>
        /// <returns>
        ///     If possible, the corresponding <see cref="INamedTypeSymbol"/> will be returned; otherwise, <see langword="null"/> will be returned.
        /// </returns>
        internal static INamedTypeSymbol GetNamedTypeSymbol(ITypeSymbol iTypeSymbol)
        {
            // Keep going until one of the cases returns.
            while (true)
            {
                // Depending on the kind of type symbol.
                switch (iTypeSymbol)
                {
                    // If it is an ArrayTypeSymbol.
                    case IArrayTypeSymbol arrayTypeSymbol:
                    {
                        // Use the element as the next type to evaluate.
                        iTypeSymbol = arrayTypeSymbol.ElementType;

                        // Break out of the switch.
                        break;
                    }

                    // If it is a PointerTypeSymbol.
                    case IPointerTypeSymbol pointerTypeSymbol:
                    {
                        // Use the element as the next type to evaluate.
                        iTypeSymbol = pointerTypeSymbol.PointedAtType;

                        // Break out of the switch.
                        break;
                    }

                    // If it is an ErrorTypeSymbol.
                    case IErrorTypeSymbol _:
                    {
                        // There is no named type here; just return null.
                        return null;
                    }

                    // If it is a NamedTypeSymbol.
                    case INamedTypeSymbol namedTypeSymbol:
                    {
                        // The named type symbol has been found; just return it.
                        return namedTypeSymbol;
                    }

                    // If it is a DynamicTypeSymbol.
                    case IDynamicTypeSymbol _:
                    {
                        // There is no named type here; just return null.
                        return null;
                    }

                    // If it is a TypeParameterSymbol.
                    case ITypeParameterSymbol _:
                    {
                        // There is no named type here; just return null.
                        return null;
                    }

                    default:
                    {
                        // Unsupported Symbol.
                        throw new NotSupportedException("The current Symbol type is not supported.");
                    }
                }
            }
        }

        /// <summary>
        ///     This is used to get the full name of the given <paramref name="iNamedTypeSymbol"/> without any prefix.
        /// </summary>
        /// <param name="iNamedTypeSymbol">
        ///     The <see cref="INamedTypeSymbol"/> 
        /// </param>
        /// <returns>
        ///     This returns the full name of the <paramref name="iNamedTypeSymbol"/> without any prefix.
        /// </returns>
        internal static string GetFullNameWithoutPrefix(this INamedTypeSymbol iNamedTypeSymbol)
        {
            // Get the fully qualified name for this named type symbol.
            string fullName = iNamedTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            // If it is not a special type.
            if (iNamedTypeSymbol.SpecialType == SpecialType.None)
            {
                // Remove the global substring from the type's full name.
                fullName = fullName.Substring(GlobalFullNamePrefix.Length);
            }

            // Return the type's full name.
            return fullName;
        }
        

        /// <summary>
        ///     This is used to get the full name of the given <paramref name="iNamespaceSymbol"/> without any prefix.
        /// </summary>
        /// <param name="iNamespaceSymbol">
        ///     The <see cref="INamespaceSymbol"/> 
        /// </param>
        /// <returns>
        ///     This returns the full name of the <paramref name="iNamespaceSymbol"/> without any prefix.
        /// </returns>
        internal static string GetFullNameWithoutPrefix(this INamespaceSymbol iNamespaceSymbol) => iNamespaceSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Substring(GlobalFullNamePrefix.Length);
    }
}
