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
        ///     This is used to get the full name of the given <paramref name="iNamedTypeSymbol"/> without any prefix.
        /// </summary>
        /// <param name="iNamedTypeSymbol">
        ///     The <see cref="INamedTypeSymbol"/> 
        /// </param>
        /// <returns>
        ///     This returns the full name of the <paramref name="iNamedTypeSymbol"/> without any prefix.
        /// </returns>
        internal static string GetFullNameWithoutPrefix(this INamedTypeSymbol iNamedTypeSymbol) => iNamedTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Substring(GlobalFullNamePrefix.Length);
    }
}
