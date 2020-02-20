namespace DotNet.SystemCollections.Analyzers.Performance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using DotNet.SystemCollections.Analyzers.Performance.SampleTypes;

    /// <summary>
    ///     This is used to gather benchmarks to compare using HashSets Vs. LINQ Distinct() fot determining uniqueness.
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
        public int CollectionSize;


        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            this.sampleIntsArray = Enumerable.Range(0, this.CollectionSize).ToArray();
            this.sampleStringsArray = Enumerable.Range(0, this.CollectionSize).Select(_ => PersonInstanceCreator.GenerateRandomStringBasedOnLength(this.CollectionSize)).ToArray();
            this.sampleSimplePersonsArray = Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).ToArray();
            this.sampleComplexPersonsArray = Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).ToArray();
            this.sampleSimplePersonsList = Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).ToList();
            this.sampleComplexPersonsList = Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).ToList();
        }

        [Benchmark]
        public void ComputingUniqueValueInIntegerArrays() => this.sampleIntsArray.Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesInStringArrays() => this.sampleStringsArray.Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValueForSimpleObjectArrays() => this.sampleSimplePersonsArray.Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForComplexObjectArrays() => this.sampleComplexPersonsArray.Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForIntegerLists() => this.sampleIntsList.Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForStringLists() => this.sampleStringsList.Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForSimpleObjectLists() => this.sampleSimplePersonsList.Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueValuesForComplexObjectLists() => this.sampleComplexPersonsList.Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public HashSet<int> ComputingUniqueValuesForIntegerHashSetsFromArray() => new HashSet<int>(this.sampleIntsArray);

        [Benchmark]
        public HashSet<int> ComputingUniqueValuesForIntegerHashSetsFromList() => new HashSet<int>(this.sampleIntsList);

        [Benchmark]
        public HashSet<string> ComputingUniqueValuesForStringHashSetsFromArray() => new HashSet<string>(this.sampleStringsArray);

        [Benchmark]
        public HashSet<string> ComputingUniqueValuesForStringHashSetsFromList() => new HashSet<string>(this.sampleStringsList);

        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueValuesForSimpleObjectHashSetsFromArray() => new HashSet<SimplePerson>(this.sampleSimplePersonsArray);

        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueValuesForSimpleObjectHashSetsFromList() => new HashSet<SimplePerson>(this.sampleSimplePersonsList);

        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueValuesForComplexHashSetsFromArray() => new HashSet<ComplexPerson>(this.sampleComplexPersonsArray);

        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueValuesForComplexHashSetsFromList() => new HashSet<ComplexPerson>(this.sampleComplexPersonsList);

        [Benchmark]
        public void ComputingUniqueIntsFromArrayExpression() => Enumerable.Repeat(this.sampleRandomGeneratedInteger, this.CollectionSize).ToArray().Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueIntsFromListExpression() => Enumerable.Repeat(this.sampleRandomGeneratedInteger, this.CollectionSize).ToList().Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueIntsFromSequenceExpression() => Enumerable.Repeat(this.sampleRandomGeneratedInteger, this.CollectionSize).Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public HashSet<int> ComputingUniqueIntsInHashSetsFromSequenceExpression() => new HashSet<int>(Enumerable.Range(0, this.CollectionSize).Select(i => i + Random.Next(0, 500)));

        [Benchmark]
        public void ComputingUniqueStringsFromArrayExpression() => Enumerable.Repeat(this.sampleRandomGenerateString, this.CollectionSize).ToArray().Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueStringsFromListExpression() => Enumerable.Repeat(this.sampleRandomGenerateString, this.CollectionSize).ToList().Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueStringsFromSequenceExpression() => Enumerable.Repeat(this.sampleRandomGenerateString, this.CollectionSize).Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public HashSet<string> ComputingUniqueStringFromHashSetExpression() => new HashSet<string>(Enumerable.Repeat(this.sampleRandomGenerateString, this.CollectionSize));

        [Benchmark]
        public void ComputingUniqueSimplePersonsFromArrayExpression() => Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).ToArray().Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueSimplePersonsFromListExpression() => Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).ToList().Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueSimplePersonsFromSequenceExpression() => Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueSimplePersonsFromHashSetExpression() => new HashSet<SimplePerson>(Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize));

        [Benchmark]
        public void ComputingUniqueComplexPersonsFromArrayExpression() => Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).ToArray().Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueComplexPersonsFromListExpression() => Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).ToList().Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public void ComputingUniqueComplexPersonsFromSequenceExpression() => Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).Distinct().Consume(this.iEnumerableConsumer);

        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueComplexPersonsFromHashSetExpression() => new HashSet<ComplexPerson>(Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize));
    }
}