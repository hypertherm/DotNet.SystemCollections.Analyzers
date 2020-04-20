namespace DotNet.SystemCollections.Analyzers.Performance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;

    /// <summary>
    ///     This is used to gather benchmarks to compare using the <see cref="IList{T}.this"/> indexer Vs. LINQ <see cref="Enumerable.Last{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> to return the last element in a sequence.
    /// </summary>
    public class ListIndexerVersusLinqLastBenchmarks
    {
        /// <summary>
        ///     This Benchmark.NET parameter controls the size of the collections.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        [Params(100, 1_000, 5_000, 10_000)]
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
        ///     Benchmark for determining worst-case performance for retrieving the last element in an <see cref="Array"/> using the LINQ <see cref="Enumerable.Last{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the last element of the <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public int GetLastItemOfArrayWithLinq() => this.sampleIntsArray.Last();

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the last element in an <see cref="Array"/> using the <see cref="IList{T}.this"/> indexer.
        /// </summary>
        /// <returns>
        ///     This returns the last element of the <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public int GetLastItemOfArrayWithIndexer() => this.sampleIntsArray[this.sampleIntsArray.Length - 1];

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the last element in an <see cref="List{T}"/> using the LINQ <see cref="Enumerable.Last{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the last element of the <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public int GetLastItemOfListWithLinq() => this.sampleIntsList.Last();

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the last element in an <see cref="List{T}"/> using the <see cref="IList{T}.this"/> indexer.
        /// </summary>
        /// <returns>
        ///     This returns the last element of the <see cref="List{T}"/>.
        /// </returns>
        [Benchmark]
        public int GetLastItemOfListWithIndexer() => this.sampleIntsList[this.sampleIntsList.Count - 1];
    }
}
