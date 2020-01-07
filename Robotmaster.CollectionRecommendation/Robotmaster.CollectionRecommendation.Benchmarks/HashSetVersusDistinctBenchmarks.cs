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
        public void ComputingUniqueValueInIntegerArrays() => sampleIntsArray.Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesinStringArrays() => sampleStringsArray.Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValueForSimpleObjectArrays() => sampleSimplePersonsArray.Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForComplexObjectArrays() => sampleComplexPersonsArray.Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForIntegerLists() => sampleIntsList.Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForStringLists() => sampleStringsList.Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForSimpleObjectLists() => sampleSimplePersonsList.Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForComplexObjectLists() => sampleComplexPersonsList.Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForIntegerHashSets()
        {

        }

        [Benchmark]
        public void ComputingUniqueValuesForStringHashSets()
        {

        }

        [Benchmark]
        public void ComputingUniqueValuesForSimpleObjectHashSets()
        {

        }

        [Benchmark]
        public void ComputingUniqueValuesForComplexHashSets()
        {

        }

        [Benchmark]
        public void ComputingUniqueValuesFromArrayExpression()
        {

        }

        [Benchmark]
        public void ComputingUniqueValuesFromListExpression()
        {

        }

        [Benchmark]
        public void ComputingUniqueValuesFromHashSetExpression()
        {

        }

        [Benchmark]
        public void ComputingUniqueValuesForSimplPersoonFromHashSetExpression()
        {

        }
    }
}
