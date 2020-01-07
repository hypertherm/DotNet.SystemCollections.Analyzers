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

        [Params(100, 1_000, 10_000, 100_000)]
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
        public void ComputingUniqueValuesInStringArrays() => sampleStringsArray.Distinct().Consume(iEnumerableConsumer);

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
        public HashSet<int> ComputingUniqueValuesForIntegerHashSetsFromArray() => new HashSet<int>(sampleIntsArray);

        [Benchmark]
        public HashSet<int> ComputingUniqueValuesForIntegerHashSetsFromList() => new HashSet<int>(sampleIntsList);

        [Benchmark]
        public HashSet<string> ComputingUniqueValuesForStringHashSetsFromArray() => new HashSet<string>(sampleStringsArray);

        [Benchmark]
        public HashSet<string> ComputingUniqueValuesForStringHashSetsFromList() => new HashSet<string>(sampleStringsList);

        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueValuesForSimpleObjectHashSetsFromArray() => new HashSet<SimplePerson>(sampleSimplePersonsArray);

        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueValuesForSimpleObjectHashSetsFromList() => new HashSet<SimplePerson>(sampleSimplePersonsList);

        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueValuesForComplexHashSetsFromArray() => new HashSet<ComplexPerson>(sampleComplexPersonsArray);

        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueValuesForComplexHashSetsFromList() => new HashSet<ComplexPerson>(sampleComplexPersonsList);

        [Benchmark]
        public void ComputingUniqueIntsFromArrayExpression() => Enumerable.Range(0, collectionSize).Select(i => i + random.Next(0, 500)).ToArray().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueIntsFromListExpression() => Enumerable.Range(0, collectionSize).Select(i => i + random.Next(0, 500)).ToList().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueIntsFromSequenceExpression() => Enumerable.Range(0, collectionSize).Select(i => i + random.Next(0, 500)).Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public HashSet<int> ComputingUniqueIntsInHashSetsFromSequenceExpression() => new HashSet<int>(Enumerable.Range(0, collectionSize).Select(i => i + random.Next(0, 500)));

        [Benchmark]
        public void ComputingUniqueStringsFromArrayExpression() => Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomStringBasedOnLength(500)).ToArray().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueStringsFromListExpression() => Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomStringBasedOnLength(500)).ToList().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueStringsFromSequenceExpression() => Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomStringBasedOnLength(500)).Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public HashSet<string> ComputingUniqueStringFromHashSetExpression() => new HashSet<string>(Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomStringBasedOnLength(500)));

        [Benchmark]
        public void ComputingUniqueSimplePersonsFromArrayExpression() => Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomSimplePerson()).ToArray().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueSimplePersonsFromListExpression() => Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomSimplePerson()).ToList().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueSimplePersonsFromSequenceExpression() => Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomSimplePerson()).Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueSimplePersonsFromHashSetExpression() => new HashSet<SimplePerson>(Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomSimplePerson()));

        [Benchmark]
        public void ComputingUniqueComplexPersonsFromArrayExpression() => Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomComplexPerson()).ToArray().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueComplexPersonsFromListExpression() => Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomComplexPerson()).ToList().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueComplexPersonsFromSequenceExpression() => Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomComplexPerson()).Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueComplexPersonsFromHashSetExpression() => new HashSet<ComplexPerson>(Enumerable.Range(0, collectionSize).Select(i => PersonInstanceCreator.GenerateRandomComplexPerson()));
    }
}