using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Robotmaster.CollectionRecommendation.Performance
{
    public class IListIndexingVersusLinqFirstBenchmarks
    {
        public int[] sampleIntsArray;
        public List<int> sampleIntsList;

        [Params(10_000, 100_000, 1_000_000)]
        public int CollectionSize;

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            sampleIntsArray = Enumerable.Range(0, CollectionSize).ToArray();
            sampleIntsList = new List<int>(Enumerable.Range(0, CollectionSize));
        }

        [Benchmark]
        public int GetFirstItemOfArrayWithLinq() => sampleIntsArray.First();

        [Benchmark]
        public int GetFirstItemOfArrayWithIndexer() => sampleIntsArray[0];

        [Benchmark]
        public int GetFirstItemOfListWithLinq() => sampleIntsList.First();

        [Benchmark]
        public int GetFirstItemOfListWithIndexer() => sampleIntsList[0];
    }
}