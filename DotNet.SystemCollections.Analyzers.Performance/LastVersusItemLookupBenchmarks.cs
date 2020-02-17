using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace ConsoleApp1
{
    public class LastVersusItemLookupBenchmarks
    {
        public int[] sampleIntsArray;
        public List<int> sampleIntsList;

        [Params( 100, 1_000, 5_000, 10_000)]
        public int collectionSize;

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            sampleIntsArray = Enumerable.Range(0, collectionSize).ToArray();
            sampleIntsList = new List<int>(Enumerable.Range(0, collectionSize));
        }

        [Benchmark]
        public int GetLastItemOfArrayWithLinq() => sampleIntsArray.Last();

        [Benchmark]
        public int GetLastItemOfArrayWithIndexer() => sampleIntsArray[sampleIntsArray.Length - 1];

        [Benchmark]
        public int GetLastItemOfListWithLinq() => sampleIntsList.Last();

        [Benchmark]
        public int GetLastItemOfListWithIndexer() => sampleIntsList[sampleIntsList.Count - 1];
    }
}
