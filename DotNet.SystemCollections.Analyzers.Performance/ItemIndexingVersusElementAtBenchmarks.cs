namespace DotNet.SystemCollections.Analyzers.Performance
{
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;

    public class IListIndexingVersusElementAtBenchmarks
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
        public int GetItemInMiddleOfArrayWithLinq() => this.SampleIntsArray.ElementAt(this.CollectionSize / 2);

        [Benchmark]
        public int GetItemInMiddleOfArrayWithIndexer() => this.SampleIntsArray[this.CollectionSize / 2];

        [Benchmark]
        public int GetItemInMiddleOfListWithLinq() => this.SampleIntsList.ElementAt(this.CollectionSize / 2);

        [Benchmark]
        public int GetItemInMiddleOfListWithIndexer() => this.SampleIntsList[0];
    }
}