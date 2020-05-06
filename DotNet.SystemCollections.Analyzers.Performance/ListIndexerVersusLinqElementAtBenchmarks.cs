namespace DotNet.SystemCollections.Analyzers.Performance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;

    /// <summary>
    ///     This is used to gather benchmarks to compare using the <see cref="IList{T}.this"/> indexer Vs. LINQ <see cref="Enumerable.ElementAt{TSource}"/> to return the middle element in a sequence.
    /// </summary>
    public class ListIndexerVersusLinqElementAtBenchmarks
    {
        /// <summary>
        ///     This Benchmark.NET parameter controls the size of the collections.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        [Params(10_000, 100_000, 1_000_000)]
        public int CollectionSize;
#pragma warning restore SA1401 // Fields should be private

        private int[] sampleIntsArray;
        private List<int> sampleIntsList;

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            this.sampleIntsArray = Enumerable.Range(0, this.CollectionSize).ToArray();
            this.sampleIntsList = new List<int>(Enumerable.Range(0, this.CollectionSize));
        }

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the middle element in an <see cref="Array"/> using the LINQ <see cref="Enumerable.ElementAt{TSource}"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the middle element of the <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public int GetItemInMiddleOfArrayWithLinqElementAtMethod() => this.sampleIntsArray.ElementAt(this.CollectionSize / 2);

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the middle element in an <see cref="Array"/> using the <see cref="IList{T}.this"/> indexer.
        /// </summary>
        /// <returns>
        ///     This returns the middle element of the <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public int GetItemInMiddleOfArrayWithIListIndexer() => this.sampleIntsArray[this.CollectionSize / 2];

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the middle element in a <see cref="List{T}"/> using the LINQ <see cref="Enumerable.ElementAt{TSource}"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the middle element of the <see cref="List{T}"/>.
        /// </returns>
        [Benchmark]
        public int GetItemInMiddleOfListWithLinqElementAtMethod() => this.sampleIntsList.ElementAt(this.CollectionSize / 2);

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the middle element in an <see cref="List{T}"/> using the <see cref="IList{T}.this"/> indexer.
        /// </summary>
        /// <returns>
        ///     This returns the middle element of the <see cref="List{T}"/>.
        /// </returns>
        [Benchmark]
        public int GetItemInMiddleOfListWithIListIndexer() => this.sampleIntsList[this.CollectionSize / 2];
    }
}