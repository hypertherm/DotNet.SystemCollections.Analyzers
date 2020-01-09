using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Robotmaster.CollectionRecommendation.Benchmarks;
using Robotmaster.CollectionRecommendation.Benchmarks.Sample_Types;

namespace ConsoleApp1
{
    /// <summary>
    ///     This is used to gather benchmarks to compare using items lookups in IList collections Vs. LINQ <see cref="Enumerable.Single{TSource}(System.Collections.Generic.IEnumerable{TSource})"/> to return the only item of the collection.
    /// </summary>
    public class SingleVersusIListCollectionItemLookup
    {
        private readonly SimplePerson sampleSimplePerson = new SimplePerson("Some First Name", "Some Last Name", DateTime.Now);
        private readonly ComplexPerson sampleComplexPerson = new ComplexPerson("Some First Complex Long Name", "Some Complex Short Name", 100, new Address("Some Street Name,", "102 203"), DateTime.Now, "Profession", "Phone Number", Citizenship.CANADA, Gender.Man, new List<string> { "coding", "sports" });

        private int[] sampleIntsArray;
        private string[] sampleStringsArray;
        private SimplePerson[] sampleSimplePersonsArray;
        private ComplexPerson[] sampleComplexPersonsArray;

        [Params(1, 5, 10, 25)]
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
        }

        [Benchmark]
        public Exception ComputeSingleOnIntsArray()
        {
            try
            {
                var singleValue = sampleIntsArray.Single();
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        } 

        [Benchmark]
        public int ComputeItemLookupOnIntsArray() => sampleIntsArray.Length == 1 ? sampleIntsArray[0] : -1;

        [Benchmark]
        public Exception ComputeSingleOnStringsArray()
        {
            try
            {
                var singleValue = sampleStringsArray.Single();
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        [Benchmark]
        public string ComputeItemLookupOnStringsArray() => sampleStringsArray.Length == 1 ? sampleStringsArray[0] : string.Empty;

        [Benchmark]
        public Exception ComputeSingleOnSimplePersonsArray()
        {
            try
            {
                var singleValue = sampleSimplePersonsArray.Single();
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        [Benchmark]
        public SimplePerson ComputeItemLookupOnSimplePersonsArray() => sampleSimplePersonsArray.Length == 1 ? sampleSimplePersonsArray[0] : null;

        [Benchmark]
        public Exception ComputeSingleOnComplexPersonsArray()
        {
            try
            {
                var singleValue = sampleComplexPersonsArray.Single();
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        [Benchmark]
        public ComplexPerson ComputeItemLookupOnComplexPersonsArray() => sampleComplexPersonsArray.Length == 1 ? sampleComplexPersonsArray[0] : null;
    }
}