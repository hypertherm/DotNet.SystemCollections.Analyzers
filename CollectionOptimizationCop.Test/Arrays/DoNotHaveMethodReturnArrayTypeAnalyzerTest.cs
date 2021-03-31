namespace CollectionOptimizationCop.Test.Arrays
{
    using CollectionOptimizationCop.ArrayAnalyzers;
    using CollectionOptimizationCop.Test.Verifiers;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     This tester is used to test the <see cref="DoNotHaveMethodReturnArrayTypeAnalyzer"/>.
    /// </summary>
    [TestClass]
    public class DoNotHaveMethodReturnArrayTypeAnalyzerTest : DiagnosticVerifier
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
            object[] MethodName()
            {
            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = DoNotHaveMethodReturnArrayTypeAnalyzer.DiagnosticId,
                Message = string.Format(DoNotHaveMethodReturnArrayTypeAnalyzer.MessageFormat, "MethodName"),
                Severity = DiagnosticSeverity.Warning,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 13, 22),
                },
            };

            this.VerifyCSharpDiagnostic(test, expected);
        }

        /// <summary>
        ///     This is used to return the appropriate C# analyzer to test.
        /// </summary>
        /// <returns>
        ///     Returns a new <see cref="DoNotHaveMethodReturnArrayTypeAnalyzer"/>.
        /// </returns>
        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DoNotHaveMethodReturnArrayTypeAnalyzer();
        }
    }
}
