using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Robotmaster.CollectionRecommendation.Performance.SampleTypes;

namespace Robotmaster.CollectionRecommendation.Performance
{
    public class LastVersusItemLookupBenchmarks
    {
        private static readonly string sampleRandomGenerateString = PersonInstanceCreator.GenerateRandomStringBasedOnLength(500);

        public string[] sampleStringArray;
        public List<string> sampleStringList;
        public ISet<string> sampleStringSet;

        [Params(5_000, 10_000)]
        public int CollectionSize;

        /// <summary>
        ///     Setup method for the benchmarks.
        /// </summary>
        [GlobalSetup]
        public void SetupStandardSession()
        {
            sampleStringArray = Enumerable.Repeat(sampleRandomGenerateString, CollectionSize).ToArray();
            sampleStringArray[sampleStringArray.Length - 1] = "Hello World";
            sampleStringList = new List<string>(Enumerable.Repeat(sampleRandomGenerateString, CollectionSize));
            sampleStringList[sampleStringList.Count - 1] = "Hello World";
            sampleStringSet = new HashSet<string>(Enumerable.Repeat(sampleRandomGenerateString, CollectionSize - 1));
            sampleStringSet.Add("Hello World");
        }

        [Benchmark]
        public bool RetrieveStringValueInArrayWithContains() => sampleStringArray.Contains("Hello World");

        [Benchmark]
        public bool RetrieveStringValueInListWithContains() => sampleStringList.Contains("Hello World");

        [Benchmark]
        public bool RetrieveStringValueInSetWithContains() => sampleStringSet.Contains("Hello World");
    }
}