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
    public class HashSetVersusLinqDistinctBenchmarks
    {
        /// <summary>
        ///     This Benchmark.NET parameter controls the size of the collections.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        [Params(5, 10, 25)]
        public int CollectionSize;
#pragma warning restore SA1401 // Fields should be private

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

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            this.sampleIntsArray = Enumerable.Range(0, this.CollectionSize).ToArray();
            this.sampleIntsList = Enumerable.Range(0, this.CollectionSize).ToList();
            this.sampleStringsArray = Enumerable.Range(0, this.CollectionSize).Select(_ => PersonInstanceCreator.GenerateRandomStringBasedOnLength(this.CollectionSize)).ToArray();
            this.sampleStringsList = Enumerable.Range(0, this.CollectionSize).Select(_ => PersonInstanceCreator.GenerateRandomStringBasedOnLength(this.CollectionSize)).ToList();
            this.sampleSimplePersonsArray = Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).ToArray();
            this.sampleComplexPersonsArray = Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).ToArray();
            this.sampleSimplePersonsList = Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).ToList();
            this.sampleComplexPersonsList = Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).ToList();
        }

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in an <see cref="int"/> <see cref="Array"/> using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueValueInIntegerArrays() => this.sampleIntsArray.Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in a <see cref="string"/> <see cref="Array"/> using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueValuesInStringArrays() => this.sampleStringsArray.Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in a simple object (i.e. <see cref="SimplePerson"/>) <see cref="Array"/> using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueValueForSimpleObjectArrays() => this.sampleSimplePersonsArray.Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in a complex object (i.e. <see cref="ComplexPerson"/>) <see cref="Array"/> using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueValuesForComplexObjectArrays() => this.sampleComplexPersonsArray.Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in a <see cref="List{T}"/> of <see cref="int"/>s using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueValuesForIntegerLists() => this.sampleIntsList.Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in a <see cref="List{T}"/> of <see cref="string"/>s using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueValuesForStringLists() => this.sampleStringsList.Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in a <see cref="List{T}"/> of simple objects (i.e. <see cref="SimplePerson"/>s) using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueValuesForSimpleObjectLists() => this.sampleSimplePersonsList.Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in a <see cref="List{T}"/> of complex objects (i.e. <see cref="ComplexPerson"/>s) using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueValuesForComplexObjectLists() => this.sampleComplexPersonsList.Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in an <see cref="int"/> <see cref="Array"/> by creating an intermediate <see cref="HashSet{T}"/> of <see cref="int"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="int"/>s based on the given <see cref="int"/> <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public HashSet<int> ComputingUniqueValuesForIntegerHashSetsFromArray() => new HashSet<int>(this.sampleIntsArray);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in an <see cref="List{T}"/> of <see cref="int"/>s by creating an intermediate <see cref="HashSet{T}"/> of <see cref="int"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="int"/>s based on the given <see cref="List{T}"/> of <see cref="int"/>s.
        /// </returns>
        [Benchmark]
        public HashSet<int> ComputingUniqueValuesForIntegerHashSetsFromList() => new HashSet<int>(this.sampleIntsList);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in an <see cref="string"/> <see cref="Array"/> by creating an intermediate <see cref="HashSet{T}"/> of <see cref="string"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="string"/>s based on the given <see cref="string"/> <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public HashSet<string> ComputingUniqueValuesForStringHashSetsFromArray() => new HashSet<string>(this.sampleStringsArray);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in an <see cref="List{T}"/> of <see cref="string"/>s by creating an intermediate <see cref="HashSet{T}"/> of <see cref="string"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="string"/>s based on the given <see cref="List{T}"/> of <see cref="string"/>s.
        /// </returns>
        [Benchmark]
        public HashSet<string> ComputingUniqueValuesForStringHashSetsFromList() => new HashSet<string>(this.sampleStringsList);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in an <see cref="SimplePerson"/> <see cref="Array"/> by creating an intermediate <see cref="HashSet{T}"/> of <see cref="SimplePerson"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="SimplePerson"/>s based on the given <see cref="SimplePerson"/> <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueValuesForSimpleObjectHashSetsFromArray() => new HashSet<SimplePerson>(this.sampleSimplePersonsArray);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in an <see cref="List{T}"/> of <see cref="SimplePerson"/>s by creating an intermediate <see cref="HashSet{T}"/> of <see cref="SimplePerson"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="SimplePerson"/>s based on the given <see cref="List{T}"/> of <see cref="SimplePerson"/>s.
        /// </returns>
        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueValuesForSimpleObjectHashSetsFromList() => new HashSet<SimplePerson>(this.sampleSimplePersonsList);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in an <see cref="ComplexPerson"/> <see cref="Array"/> by creating an intermediate <see cref="HashSet{T}"/> of <see cref="ComplexPerson"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="ComplexPerson"/>s based on the given <see cref="ComplexPerson"/> <see cref="Array"/>.
        /// </returns>
        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueValuesForComplexObjectHashSetsFromArray() => new HashSet<ComplexPerson>(this.sampleComplexPersonsArray);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements in an <see cref="List{T}"/> of <see cref="ComplexPerson"/>s by creating an intermediate <see cref="HashSet{T}"/> of <see cref="ComplexPerson"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="ComplexPerson"/>s based on the given <see cref="List{T}"/> of <see cref="ComplexPerson"/>s.
        /// </returns>
        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueValuesForComplexObjectHashSetsFromList() => new HashSet<ComplexPerson>(this.sampleComplexPersonsList);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that generates an <see cref="int"/> <see cref="Array"/> with one value, <see cref="sampleRandomGeneratedInteger"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueIntsFromArrayExpression() => Enumerable.Repeat(this.sampleRandomGeneratedInteger, this.CollectionSize).ToArray().Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that generates a <see cref="List{T}"/> of <see cref="int"/>s with one value, <see cref="sampleRandomGeneratedInteger"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueIntsFromListExpression() => Enumerable.Repeat(this.sampleRandomGeneratedInteger, this.CollectionSize).ToList().Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that created a lazily-generated sequence with one value, <see cref="sampleRandomGeneratedInteger"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueIntsFromSequenceExpression() => Enumerable.Repeat(this.sampleRandomGeneratedInteger, this.CollectionSize).Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from a lazily-generated sequence of <see cref="int"/>s, by creating an intermediate <see cref="HashSet{T}"/> of <see cref="int"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="int"/>s based on the given lazily-generated sequence of <see cref="int"/>s.
        /// </returns>
        [Benchmark]
        public HashSet<int> ComputingUniqueIntsInHashSetsFromSequenceExpression() => new HashSet<int>(Enumerable.Range(0, this.CollectionSize).Select(i => i + Random.Next(0, 500)));

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that generates an <see cref="string"/> <see cref="Array"/> with one value, <see cref="sampleRandomGenerateString"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueStringsFromArrayExpression() => Enumerable.Repeat(this.sampleRandomGenerateString, this.CollectionSize).ToArray().Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that generates a <see cref="List{T}"/> of <see cref="string"/>s with one value, <see cref="sampleRandomGenerateString"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueStringsFromListExpression() => Enumerable.Repeat(this.sampleRandomGenerateString, this.CollectionSize).ToList().Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that created a lazily-generated sequence with one value, <see cref="sampleRandomGenerateString"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueStringsFromSequenceExpression() => Enumerable.Repeat(this.sampleRandomGenerateString, this.CollectionSize).Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that created a lazily-generated sequence with one value, <see cref="sampleRandomGenerateString"/>, repeated <see cref="CollectionSize"/> times, by creating an intermediate <see cref="HashSet{T}"/> of <see cref="string"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="string"/>s based on the given lazily-generated sequence of <see cref="string"/>s.
        /// </returns>
        [Benchmark]
        public HashSet<string> ComputingUniqueStringFromHashSetExpression() => new HashSet<string>(Enumerable.Repeat(this.sampleRandomGenerateString, this.CollectionSize));

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that generates an <see cref="SimplePerson"/> <see cref="Array"/> with one value, <see cref="sampleSimplePerson"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueSimplePersonsFromArrayExpression() => Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).ToArray().Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that generates a <see cref="List{T}"/> of <see cref="SimplePerson"/>s with one value, <see cref="sampleSimplePerson"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueSimplePersonsFromListExpression() => Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).ToList().Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that created a lazily-generated sequence with one value, <see cref="sampleSimplePerson"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueSimplePersonsFromSequenceExpression() => Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize).Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that created a lazily-generated sequence with one value, <see cref="sampleSimplePerson"/>, repeated <see cref="CollectionSize"/> times, by creating an intermediate <see cref="HashSet{T}"/> of <see cref="SimplePerson"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="SimplePerson"/>s based on the given lazily-generated sequence of <see cref="SimplePerson"/>s.
        /// </returns>
        [Benchmark]
        public HashSet<SimplePerson> ComputingUniqueSimplePersonsFromHashSetExpression() => new HashSet<SimplePerson>(Enumerable.Repeat(this.sampleSimplePerson, this.CollectionSize));

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that generates an <see cref="ComplexPerson"/> <see cref="Array"/> with one value, <see cref="sampleComplexPerson"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueComplexPersonsFromArrayExpression() => Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).ToArray().Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that generates a <see cref="List{T}"/> of <see cref="ComplexPerson"/>s with one value, <see cref="sampleComplexPerson"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueComplexPersonsFromListExpression() => Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).ToList().Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that created a lazily-generated sequence with one value, <see cref="sampleComplexPerson"/>, repeated <see cref="CollectionSize"/> times, using the LINQ <see cref="Enumerable.Distinct{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> extension method.
        /// </summary>
        [Benchmark]
        public void ComputingUniqueComplexPersonsFromSequenceExpression() => Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize).Distinct().Consume(this.iEnumerableConsumer);

        /// <summary>
        ///     Benchmark for determining performance of computing the distinct elements, from an <see cref="Enumerable.Repeat{TResult}"/> that created a lazily-generated sequence with one value, <see cref="sampleComplexPerson"/>, repeated <see cref="CollectionSize"/> times, by creating an intermediate <see cref="HashSet{T}"/> of <see cref="ComplexPerson"/>s from it.
        /// </summary>
        /// <returns>
        ///     The newly created, <see cref="HashSet{T}"/> of <see cref="ComplexPerson"/>s based on the given lazily-generated sequence of <see cref="ComplexPerson"/>s.
        /// </returns>
        [Benchmark]
        public HashSet<ComplexPerson> ComputingUniqueComplexPersonsFromHashSetExpression() => new HashSet<ComplexPerson>(Enumerable.Repeat(this.sampleComplexPerson, this.CollectionSize));
    }
}