namespace DotNet.SystemCollections.Analyzers.Performance
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;

    /// <summary>
    ///     This is used to gather benchmarks to compare using the <see cref="ICollection.Count"/> property Vs. LINQ <see cref="Enumerable.Count{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> to return the number of elements in a collection.
    /// </summary>
    public class CollectionCountVersusLinqCountBenchmarks
    {
        private int[] sampleArray;
        private List<int> sampleList;
        private LinkedList<int> sampleLinkedList;
        private Dictionary<int, int> sampleDictionary;
        private HashSet<int> sampleHashSet;
        private Queue<int> sampleQueue;
        private Stack<int> sampleStack;

        [Params(25, 50, 100)]
        public int CollectionSize;

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

        [Benchmark]
        public int ComputingNumberOfElementsInArrayUsingCollectionCount() => this.sampleArray.Length;

        [Benchmark]
        public int ComputingNumberOfElementsInArrayUsingLinqCount() => this.sampleArray.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInListUsingCollectionCount() => this.sampleList.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInListUsingLinqCount() => this.sampleList.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInLinkedListUsingCollectionCount() => this.sampleLinkedList.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInLinkedListUsingLinqCount() => this.sampleLinkedList.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInDictionaryUsingCollectionCount() => this.sampleDictionary.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInDictionaryUsingLinqCount() => this.sampleDictionary.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInHashSetUsingCollectionCount() => this.sampleHashSet.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInHashSetUsingLinqCount() => this.sampleHashSet.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInQueueUsingCollectionCount() => this.sampleQueue.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInQueueUsingLinqCount() => this.sampleQueue.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInStackUsingCollectionCount() => this.sampleStack.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInStackUsingLinqCount() => this.sampleStack.Count();
    }
}
