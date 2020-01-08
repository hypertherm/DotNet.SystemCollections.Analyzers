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
        private static readonly Random Random = new Random();
        private readonly Consumer iEnumerableConsumer = new Consumer();
        private readonly SimplePerson sampleSimplePerson = new SimplePerson("Some First Name", "Some Last Name", DateTime.Now);
        private readonly ComplexPerson sampleComplexPerson = new ComplexPerson("Some First Complex Long Name", "Some Complex Short Name", 100, new Address("Some Street Name,", "102 203"), DateTime.Now, "Profession", "Phone Number", Citizenship.CANADA, Gender.Man, new List<string> { "coding", "sports" });
        private readonly string sampleRandomGenerateString = PersonInstanceCreator.GenerateRandomStringBasedOnLength(500);
        private readonly int sampleRandomGeneratedInteger = Random.Next(0, 500);

        private int[] sampleIntsArray;
        private string[] sampleStringsArray;
        private SimplePerson[] sampleSimplePersonsArray;
        private ComplexPerson[] sampleComplexPersonsArray;

        private List<int> sampleIntsList;
        private List<string> sampleStringsList;
        private List<SimplePerson> sampleSimplePersonsList;
        private List<ComplexPerson> sampleComplexPersonsList;

        [Params(5, 10, 25)]
        public int collectionSize;


        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            sampleIntsArray = Enumerable.Range(0, collectionSize).ToArray();
            sampleStringsArray = Enumerable.Range(0, collectionSize).Select(_ => PersonInstanceCreator.GenerateRandomStringBasedOnLength(collectionSize)).ToArray();
            sampleSimplePersonsArray = Enumerable.Repeat(sampleSimplePerson, collectionSize).ToArray();
            sampleComplexPersonsArray = Enumerable.Repeat(sampleComplexPerson, collectionSize).ToArray();
            sampleSimplePersonsList = Enumerable.Repeat(sampleSimplePerson, collectionSize).ToList();
            sampleComplexPersonsList = Enumerable.Repeat(sampleComplexPerson, collectionSize).ToList();
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
        public void ComputingUniqueIntsFromArrayExpression() => Enumerable.Repeat(sampleRandomGeneratedInteger, collectionSize).ToArray().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueIntsFromListExpression() => Enumerable.Repeat(sampleRandomGeneratedInteger, collectionSize).ToList().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueIntsFromSequenceExpression() => Enumerable.Repeat(sampleRandomGeneratedInteger, collectionSize).Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public HashSet<int> ComputingUniqueIntsInHashSetsFromSequenceExpression() => new HashSet<int>(Enumerable.Range(0, collectionSize).Select(i => i + Random.Next(0, 500)));

        [Benchmark]
        public void ComputingUniqueStringsFromArrayExpression() => Enumerable.Repeat(sampleRandomGenerateString, collectionSize).ToArray().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueStringsFromListExpression() => Enumerable.Repeat(sampleRandomGenerateString, collectionSize).ToList().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueStringsFromSequenceExpression() => Enumerable.Repeat(sampleRandomGenerateString, collectionSize).Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public HashSet<string> ComputingUniqueStringFromHashSetExpression() => new HashSet<string>(Enumerable.Repeat(sampleRandomGenerateString, collectionSize));

        [Benchmark]
        public void ComputingUniqueSimplePersonsFromArrayExpression() => Enumerable.Repeat(sampleSimplePerson, collectionSize).ToArray().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueSimplePersonsFromListExpression() => Enumerable.Repeat(sampleSimplePerson, collectionSize).ToList().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueSimplePersonsFromSequenceExpression() => Enumerable.Repeat(sampleSimplePerson, collectionSize).Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueSimplePersonsFromHashSetExpression() => new HashSet<SimplePerson>(Enumerable.Repeat(sampleSimplePerson, collectionSize));

        [Benchmark]
        public void ComputingUniqueComplexPersonsFromArrayExpression() => Enumerable.Repeat(sampleComplexPerson, collectionSize).ToArray().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueComplexPersonsFromListExpression() => Enumerable.Repeat(sampleComplexPerson, collectionSize).ToList().Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueComplexPersonsFromSequenceExpression() => Enumerable.Repeat(sampleComplexPerson, collectionSize).Distinct().Consume(iEnumerableConsumer);

        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueComplexPersonsFromHashSetExpression() => new HashSet<ComplexPerson>(Enumerable.Repeat(sampleComplexPerson, collectionSize));
    }
}