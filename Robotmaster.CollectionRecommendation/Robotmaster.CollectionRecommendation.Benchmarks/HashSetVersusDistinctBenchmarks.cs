using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Robotmaster.CollectionRecommendation.Benchmarks.Sample_Types;

namespace Robotmaster.CollectionRecommendation.Benchmarks
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     In the case of HashSets, the purpose here is to show the "power" of using set based collection over invoking the Distinct method.
    ///     This benchmark will not cover adding custom equality comparer to supply to the collection.
    /// </remarks>
    public class HashSetVersusDistinctBenchmarks
    {
        private static readonly Random random = new Random();
        private readonly Consumer iEnumerableConsumer = new Consumer();

        private int[] sampleIntsArray;
        private string[] sampleStringsArray;
        private SimplePerson[] sampleSimplePersonsArray;
        private ComplexPerson[] sampleComplexPersonsArray;

        private List<int> sampleIntsList;
        private List<string> sampleStringsList;
        private List<SimplePerson> sampleSimplePersonsList;
        private List<ComplexPerson> sampleComplexPersonsList;

        [Params(100, 1_000, 10_000)]
        public int collectionSize;


        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            sampleIntsArray = Enumerable.Range(0, collectionSize).ToArray();
            sampleStringsArray = Enumerable.Range(0, collectionSize).Select(_ => PersonInstanceCreator.GenerateRandomStringBasedOnLength(collectionSize)).ToArray();
            sampleSimplePersonsArray = PersonInstanceCreator.GenerateRandomSimplePersons(collectionSize).ToArray();
            sampleComplexPersonsArray = PersonInstanceCreator.GenerateRandomComplexPersons(collectionSize).ToArray();

            sampleIntsList = Enumerable.Range(0, collectionSize).ToList();
            sampleStringsList = Enumerable.Range(0, collectionSize).Select(_ => PersonInstanceCreator.GenerateRandomStringBasedOnLength(collectionSize)).ToList();
            sampleSimplePersonsList = PersonInstanceCreator.GenerateRandomSimplePersons(collectionSize);
            sampleComplexPersonsList = PersonInstanceCreator.GenerateRandomComplexPersons(collectionSize);
        }

        [Benchmark]
        public IEnumerable<int> ComputingUniqueValueInIntegerArrays()
        {

        }

        [Benchmark]
        public IEnumerable<string> ComputingUniqueValuesinStringArrays()
        {

        }

        [Benchmark]
        public IEnumerable<SimplePerson> ComputingUniqueValueForSimpleObjectArrays()
        {

        }

        [Benchmark]
        public IEnumerable<ComplexPerson> ComputingUniqueValuesForComplexObjectArrays()
        {

        }

        [Benchmark]
        public IEnumerable<int> ComputingUniqueValuesForIntegerLists()
        {

        }

        [Benchmark]
        public IEnumerable<string> ComputingUniqueValuesForStringLists()
        {

        }

        [Benchmark]
        public IEnumerable<SimplePerson> ComputingUniqueValuesForSimpleObjectLists()
        {

        }

        [Benchmark]
        public IEnumerable<ComplexPerson> ComputingUniqueValuesForComplexObjectLists()
        {

        }

        [Benchmark]
        public IEnumerable<int> ComputingUniqueValuesForIntegerHashSets()
        {

        }

        [Benchmark]
        public IEnumerable<string> ComputingUniqueValuesForStringHashSets()
        {

        }

        [Benchmark]
        public IEnumerable<SimplePerson> ComputingUniqueValuesForSimpleObjectHashSets()
        {

        }

        [Benchmark]
        public IEnumerable<ComplexPerson> ComputingUniqueValuesForComplexHashSets()
        {

        }

        [Benchmark]
        public IEnumerable<int> ComputingUniqueValuesFromArrayExpression()
        {

        }

        [Benchmark]
        public IEnumerable<string> ComputingUniqueValuesFromListExpression()
        {

        }

        [Benchmark]
        public IEnumerable<SimplePerson> ComputingUniqueValuesForSimplePersoonFromHashSetExpression()
        {

        }

        [Benchmark]
        public IEnumerable<ComplexPerson> ComputingUniqueValuesForComplexPersoonFromHashSetExpression()
        {

        }
    }
}
