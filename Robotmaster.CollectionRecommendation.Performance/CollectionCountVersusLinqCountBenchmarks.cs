namespace Robotmaster.CollectionRecommendation.Performance
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
        public int collectionSize;

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            sampleArray = Enumerable.Range(0, collectionSize).ToArray();
            sampleList = Enumerable.Range(0, collectionSize).ToList();
            sampleLinkedList = new LinkedList<int>(Enumerable.Range(0, collectionSize));
            sampleDictionary = Enumerable.Range(0, collectionSize).ToDictionary(x => x);
            sampleHashSet = new HashSet<int>(Enumerable.Range(0, collectionSize));
            sampleQueue = new Queue<int>(Enumerable.Range(0, collectionSize));
            sampleStack = new Stack<int>(Enumerable.Range(0, collectionSize));
        }

        [Benchmark]
        public int ComputingNumberOfElementsInArrayUsingCollectionCount() => sampleArray.Length;

        [Benchmark]
        public int ComputingNumberOfElementsInArrayUsingLinqCount() => sampleArray.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInListUsingCollectionCount() => sampleList.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInListUsingLinqCount() => sampleList.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInLinkedListUsingCollectionCount() => sampleLinkedList.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInLinkedListUsingLinqCount() => sampleLinkedList.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInDictionaryUsingCollectionCount() => sampleDictionary.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInDictionaryUsingLinqCount() => sampleDictionary.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInHashSetUsingCollectionCount() => sampleHashSet.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInHashSetUsingLinqCount() => sampleHashSet.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInQueueUsingCollectionCount() => sampleQueue.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInQueueUsingLinqCount() => sampleQueue.Count();

        [Benchmark]
        public int ComputingNumberOfElementsInStackUsingCollectionCount() => sampleStack.Count;

        [Benchmark]
        public int ComputingNumberOfElementsInStackUsingLinqCount() => sampleStack.Count();
    }
}
