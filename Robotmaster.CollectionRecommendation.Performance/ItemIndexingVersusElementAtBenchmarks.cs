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
        public int GetItemInMiddleOfArrayWithLinq() => sampleIntsArray.ElementAt(CollectionSize / 2);

        [Benchmark]
        public int GetItemInMiddleOfArrayWithIndexer() => sampleIntsArray[CollectionSize / 2];

        [Benchmark]
        public int GetItemInMiddleOfListWithLinq() => sampleIntsList.ElementAt(CollectionSize / 2);

        [Benchmark]
        public int GetItemInMiddleOfListWithIndexer() => sampleIntsList[0];
    }
}