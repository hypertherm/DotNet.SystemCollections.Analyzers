namespace CollectionOptimizationCop.Test.Arrays
{
    using CollectionOptimizationCop.ArrayAnalyzers;
    using CollectionOptimizationCop.Test.Verifiers;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     This tester is used to test the <see cref="DoNotHaveFieldOfArrayTypeAnalyzer"/>.
    /// </summary>
    [TestClass]
    public class DoNotHaveFieldOfArrayTypeAnalyzerTest : DiagnosticVerifier
    {
        /// <summary>
        ///     Test when given an empty input.
        /// </summary>
        /// <remarks>
        ///     No diagnostics expected to show up.
        /// </remarks>
        [TestMethod]
        public void TestEmptyInput()
        {
            var test = string.Empty;

            this.VerifyCSharpDiagnostic(test);
        }

        /// <summary>
        ///     Test when given a matching case.
        /// </summary>
        /// <remarks>
        ///     Diagnostic triggered and checked for.
        /// </remarks>
        [TestMethod]
        public void TestMatchingCase()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class TypeName
        {
            private object[] field;
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = DoNotHaveFieldOfArrayTypeAnalyzer.DiagnosticId,
                Message = string.Format(DoNotHaveFieldOfArrayTypeAnalyzer.MessageFormat, "field"),
                Severity = DiagnosticSeverity.Warning,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 13, 30),
                },
            };

            this.VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        ///     Gets the relevant C# diagnostic analyzer to test.
        /// </summary>
        /// <returns>
        ///     This returns a new <see cref="DoNotHaveFieldOfArrayTypeAnalyzer"/>.
        /// </returns>
        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DoNotHaveFieldOfArrayTypeAnalyzer();
        }
    }
}