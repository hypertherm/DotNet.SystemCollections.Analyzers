namespace DotNet.SystemCollections.Analyzers.Performance
{
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using DotNet.SystemCollections.Analyzers.Performance.SampleTypes;

    /// <summary>
    ///     This benchmark provider generates benchmarks comparing the worst-case performance of Contains() on generic sequences (Lists, arrays) against that of Contains() on a Set.
    /// </summary>
    public class HashSetVersusLinqContainsBenchmarks
    {
        /// <summary>
        ///     This Benchmark.NET parameter controls the size of the collections.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        [Params(5_000, 10_000)]
        public int CollectionSize;
#pragma warning restore SA1401 // Fields should be private

        private const string SearchTarget = "Hello World";
        private static readonly string SampleRandomGenerateString = PersonInstanceCreator.GenerateRandomStringBasedOnLength(500);

        private string[] sampleStringArray;
        private List<string> sampleStringList;
        private ISet<string> sampleStringSet;

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            this.sampleStringArray = Enumerable.Repeat(SampleRandomGenerateString, this.CollectionSize).ToArray();
            this.sampleStringArray[this.sampleStringArray.Length - 1] = SearchTarget;
            this.sampleStringList = new List<string>(Enumerable.Repeat(SampleRandomGenerateString, this.CollectionSize));
            this.sampleStringList[this.sampleStringList.Count - 1] = SearchTarget;
            this.sampleStringSet = new HashSet<string>(Enumerable.Repeat(SampleRandomGenerateString, this.CollectionSize - 1));
            this.sampleStringSet.Add(SearchTarget);
        }

        /// <summary>
        ///     Benchmark for determining worst-case performance of running a Contains() on an array.
        /// </summary>
        /// <returns>
        ///     This returns a <see cref="bool"/>, indicating whether or not the search target was found (should be true.)
        /// </returns>
        [Benchmark]
        public bool RetrieveStringValueInArrayWithContains() => this.sampleStringArray.Contains(SearchTarget);

        /// <summary>
        ///     Benchmark for determining worst-case performance of running a Contains() on a List.
        /// </summary>
        /// <returns>
        ///     This returns a <see cref="bool"/>, indicating whether or not the search target was found (should be true.)
        /// </returns>
        [Benchmark]
        public bool RetrieveStringValueInListWithContains() => this.sampleStringList.Contains(SearchTarget);

        /// <summary>
        ///     Benchmark for determining worst-case performance of running a Contains() on a Set.
        /// </summary>
        /// <returns>
        ///     This returns a <see cref="bool"/>, indicating whether or not the search target was found (should be true.)
        /// </returns>
        [Benchmark]
        public bool RetrieveStringValueInSetWithContains() => this.sampleStringSet.Contains(SearchTarget);
    }
}