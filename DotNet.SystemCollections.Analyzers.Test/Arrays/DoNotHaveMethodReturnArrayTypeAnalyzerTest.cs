using System;
using DotNet.SystemCollections.Analyzers.Arrays;
using DotNet.SystemCollections.Analyzers.Test.Verifiers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNet.SystemCollections.Analyzers.Test.Arrays
{
    [TestClass]
    public class DoNotHaveMethodReturnArrayTypeAnalyzerTest : DiagnosticVerifier
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
            object[] MethodName()
            {
            }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = DoNotHaveMethodReturnArrayTypeAnalyzer.DiagnosticId,
                Message = String.Format(DoNotHaveMethodReturnArrayTypeAnalyzer.MessageFormat, "MethodName"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 13, 22)
                    }
            };

            this.VerifyCSharpDiagnostic(test, expected);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new DoNotHaveMethodReturnArrayTypeAnalyzer();
        }
    }
}
