namespace DotNet.SystemCollections.Analyzers.Performance
{
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;

    public class LastVersusItemLookupBenchmarks
    {
        public int[] SampleIntsArray;
        public List<int> SampleIntsList;

        [Params( 100, 1_000, 5_000, 10_000)]
        public int CollectionSize;

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            this.SampleIntsArray = Enumerable.Range(0, this.CollectionSize).ToArray();
            this.SampleIntsList = new List<int>(Enumerable.Range(0, this.CollectionSize));
        }

        [Benchmark]
        public int GetLastItemOfArrayWithLinq() => this.SampleIntsArray.Last();

        [Benchmark]
        public int GetLastItemOfArrayWithIndexer() => this.SampleIntsArray[this.SampleIntsArray.Length - 1];

        [Benchmark]
        public int GetLastItemOfListWithLinq() => this.SampleIntsList.Last();

        [Benchmark]
        public int GetLastItemOfListWithIndexer() => this.SampleIntsList[this.SampleIntsList.Count - 1];
    }
}
