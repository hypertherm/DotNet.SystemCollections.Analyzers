namespace CollectionOptimizationCop.Performance
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using BenchmarkDotNet.Attributes;

    /// <summary>
    ///     This is used to gather benchmarks to compare using the <see cref="ICollection.Count"/> property Vs. LINQ <see cref="Enumerable.Count{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> to return the number of elements in a collection.
    /// </summary>
    public class CollectionCountVersusLinqCountBenchmarks
    {
        /// <summary>
        ///     This Benchmark.NET parameter controls the size of the collections.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        [Params(25, 50, 100)]
        public int CollectionSize;
#pragma warning restore SA1401 // Fields should be private

        private int[] sampleArray;
        private List<int> sampleList;
        private LinkedList<int> sampleLinkedList;
        private Dictionary<int, int> sampleDictionary;
        private HashSet<int> sampleHashSet;
        private Queue<int> sampleQueue;
        private Stack<int> sampleStack;

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            this.sampleArray = Enumerable.Range(0, this.CollectionSize).ToArray();
            this.sampleList = Enumerable.Range(0, this.CollectionSize).ToList();
            this.sampleLinkedList = new LinkedList<int>(Enumerable.Range(0, this.CollectionSize));
            this.sampleDictionary = Enumerable.Range(0, this.CollectionSize).ToDictionary(x => x);
            this.sampleHashSet = new HashSet<int>(Enumerable.Range(0, this.CollectionSize));
            this.sampleQueue = new Queue<int>(Enumerable.Range(0, this.CollectionSize));
            this.sampleStack = new Stack<int>(Enumerable.Range(0, this.CollectionSize));
        }

        /// <summary>
        ///     Benchmark for determining performance of computing the number of elements in an <see cref="Array"/> using the <see cref="Array.Length"/> property.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="Array"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInArrayUsingLengthProperty() => this.sampleArray.Length;

        /// <summary>
        ///     Benchmark for determining performance of computing the number of elements in an <see cref="Array"/> using the LINQ <see cref="Enumerable.Count{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="Array"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInArrayUsingLinqCountMethod() => this.sampleArray.Count();

        /// <summary>
        ///     This benchmark determines the performance of computing the number of elements in a <see cref="IList{T}"/> using the <see cref="ICollection.Count"/> property.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="IList{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInListUsingCollectionCountProperty() => this.sampleList.Count;

        /// <summary>
        ///     Benchmark for determining performance of computing the number of elements in a <see cref="IList{T}"/> using the LINQ <see cref="Enumerable.Count{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="IList{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInListUsingLinqCountMethod() => this.sampleList.Count();

        /// <summary>
        ///     This benchmark determines the performance of computing the number of elements in a <see cref="LinkedList{T}"/> using the <see cref="ICollection.Count"/> property.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="LinkedList{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInLinkedListUsingCollectionCountProperty() => this.sampleLinkedList.Count;

        /// <summary>
        ///     Benchmark for determining performance of computing the number of elements in a <see cref="LinkedList{T}"/> using the LINQ <see cref="Enumerable.Count{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="LinkedList{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInLinkedListUsingLinqCountMethod() => this.sampleLinkedList.Count();

        /// <summary>
        ///     This benchmark determines the performance of computing the number of elements in a <see cref="IDictionary{TKey,TValue}"/> using the <see cref="ICollection.Count"/> property.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="IDictionary{TKey,TValue}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInDictionaryUsingCollectionCountProperty() => this.sampleDictionary.Count;

        /// <summary>
        ///     Benchmark for determining performance of computing the number of elements in a <see cref="IDictionary{TKey,TValue}"/> using the LINQ <see cref="Enumerable.Count{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="IDictionary{TKey,TValue}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInDictionaryUsingLinqCountMethod() => this.sampleDictionary.Count();

        /// <summary>
        ///     This benchmark determines the performance of computing the number of elements in a <see cref="ISet{T}"/> using the <see cref="ICollection.Count"/> property.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="ISet{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInSetUsingCollectionCountProperty() => this.sampleHashSet.Count;

        /// <summary>
        ///     This benchmark determines the performance of computing the number of elements in a <see cref="ISet{T}"/> using the LINQ <see cref="Enumerable.Count{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="ISet{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInSetUsingLinqCountMethod() => this.sampleHashSet.Count();

        /// <summary>
        ///     This benchmark determines the performance of computing the number of elements in a <see cref="Queue{T}"/> using the <see cref="ICollection.Count"/> property.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="Queue{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInQueueUsingCollectionCountProperty() => this.sampleQueue.Count;

        /// <summary>
        ///     This benchmark determines the performance of computing the number of elements in a <see cref="Queue{T}"/> using the LINQ <see cref="Enumerable.Count{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="Queue{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInQueueUsingLinqCountMethod() => this.sampleQueue.Count();

        /// <summary>
        ///     This benchmark determines the performance of computing the number of elements in a <see cref="Stack{T}"/> using the <see cref="ICollection.Count"/> property.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="Stack{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInStackUsingCollectionCountProperty() => this.sampleStack.Count;

        /// <summary>
        ///     This benchmark determines the performance of computing the number of elements in a <see cref="Stack{T}"/> using the LINQ <see cref="Enumerable.Count{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        /// <returns>
        ///     This returns the number of elements in the <see cref="Stack{T}"/> (should equal <see cref="CollectionSize"/>.)
        /// </returns>
        [Benchmark]
        public int ComputingNumberOfElementsInStackUsingLinqCountMethod() => this.sampleStack.Count();
    }
}
