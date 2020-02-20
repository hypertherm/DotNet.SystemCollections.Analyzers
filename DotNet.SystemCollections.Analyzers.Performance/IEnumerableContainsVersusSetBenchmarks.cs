namespace DotNet.SystemCollections.Analyzers.Performance
{
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using DotNet.SystemCollections.Analyzers.Performance.SampleTypes;

    public class IEnumerableContainsVersusSetBenchmarks
    {
        private static readonly string SampleRandomGenerateString = PersonInstanceCreator.GenerateRandomStringBasedOnLength(500);

        public string[] SampleStringArray;
        public List<string> SampleStringList;
        public ISet<string> SampleStringSet;

        [Params(5_000, 10_000)]
        public int CollectionSize;

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            this.SampleStringArray = Enumerable.Repeat(SampleRandomGenerateString, this.CollectionSize).ToArray();
            this.SampleStringArray[this.SampleStringArray.Length - 1] = "Hello World";
            this.SampleStringList = new List<string>(Enumerable.Repeat(SampleRandomGenerateString, this.CollectionSize));
            this.SampleStringList[this.SampleStringList.Count - 1] = "Hello World";
            this.SampleStringSet = new HashSet<string>(Enumerable.Repeat(SampleRandomGenerateString, this.CollectionSize - 1));
            this.SampleStringSet.Add("Hello World");
        }

        [Benchmark]
        public bool RetrieveStringValueInArrayWithContains() => this.SampleStringArray.Contains("Hello World");

        [Benchmark]
        public bool RetrieveStringValueInListWithContains() => this.SampleStringList.Contains("Hello World");

        [Benchmark]
        public bool RetrieveStringValueInSetWithContains() => this.SampleStringSet.Contains("Hello World");
    }
}