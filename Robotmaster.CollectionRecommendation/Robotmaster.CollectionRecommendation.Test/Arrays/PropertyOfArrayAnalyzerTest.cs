using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robotmaster.CollectionRecommendation.Arrays;
using Robotmaster.CollectionRecommendation.Test.Verifiers;

namespace Robotmaster.CollectionRecommendation.Test.Arrays
{
    [TestClass]
    public class PropertyOfArrayAnalyzerTest : DiagnosticVerifier
    {
        //No diagnostics expected to show up
        [TestMethod]
        public void TestEmptyInput()
        {
            var test = @"";

            VerifyCSharpDiagnostic(test);
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
                Id = PropertyOfArrayAnalyzer.DiagnosticId,
                Message = String.Format(PropertyOfArrayAnalyzer.MessageFormat, "Property"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[]
                    {
                        new DiagnosticResultLocation("Test0.cs", 13, 30)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new PropertyOfArrayAnalyzer();
        }
    }
}
