using System;
using DotNet.SystemCollections.Analyzers.Test.Verifiers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNet.SystemCollections.Analyzers.Test.Arrays
{
    [TestClass]
    public class DoNotHavePropertyOfArrayTypeAnalyzerTest : DiagnosticVerifier
    {
        //No diagnostics expected to show up
        [TestMethod]
        public void TestEmptyInput()
        {
            var test = @"";

            this.VerifyCSharpDiagnostic(test);
        }

        //Diagnostic triggered and checked for
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
            private object[] Property { get; set; }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = DoNotHavePropertyOfArrayTypeAnalyzer.DiagnosticId,
                Message = String.Format(DoNotHavePropertyOfArrayTypeAnalyzer.MessageFormat, "Property"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 13, 30)
                    }
            };

            this.VerifyCSharpDiagnostic(test, expected);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DoNotHavePropertyOfArrayTypeAnalyzer();
        }
    }
}
