namespace DotNet.SystemCollections.Analyzers.Performance
{
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;

    public class IListIndexingVersusLinqFirstBenchmarks
    {
        public int[] SampleIntsArray;
        public List<int> SampleIntsList;

        [Params(10_000, 100_000, 1_000_000)]
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
        public int GetFirstItemOfArrayWithLinq() => this.SampleIntsArray.First();

        [Benchmark]
        public int GetFirstItemOfArrayWithIndexer() => this.SampleIntsArray[0];

        [Benchmark]
        public int GetFirstItemOfListWithLinq() => this.SampleIntsList.First();

        [Benchmark]
        public int GetFirstItemOfListWithIndexer() => this.SampleIntsList[0];
    }
}