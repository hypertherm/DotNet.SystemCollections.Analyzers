namespace CollectionOptimizationCop.Performance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    /// <summary>
    ///     This is used to gather benchmarks to compare using the <see cref="IList{T}.this"/> indexer Vs. LINQ <see cref="Enumerable.First{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> to return the first element in a sequence.
    /// </summary>
    public class ListIndexerVersusLinqFirstBenchmarks
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
        ///     Benchmark for determining worst-case performance for retrieving the first element in an <see cref="Array"/> using the LINQ <see cref="Enumerable.First{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the first element of the <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public int GetFirstItemOfArrayWithLinqFirstMethod() => this.sampleIntsArray.First();

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the first element in an <see cref="Array"/> using the <see cref="IList{T}.this"/> indexer.
        /// </summary>
        /// <returns>
        ///     This returns the first element of the <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public int GetFirstItemOfArrayWithListIndexer() => this.sampleIntsArray[0];

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the first element in an <see cref="List{T}"/> using the LINQ <see cref="Enumerable.First{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the first element of the <see cref="List{T}"/>.
        /// </returns>
        [Benchmark]
        public int GetFirstItemOfListWithLinqFirstMethod() => this.sampleIntsList.First();

        /// <summary>
        ///     Benchmark for determining worst-case performance for retrieving the first element in an <see cref="List{T}"/> using the <see cref="IList{T}.this"/> indexer.
        /// </summary>
        /// <returns>
        ///     This returns the first element of the <see cref="List{T}"/>.
        /// </returns>
        [Benchmark]
        public int GetFirstItemOfListWithListIndexer() => this.sampleIntsList[0];
    }
}