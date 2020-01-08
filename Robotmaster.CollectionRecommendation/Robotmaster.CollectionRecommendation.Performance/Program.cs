using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Robotmaster.CollectionRecommendation.Benchmarks;
using Robotmaster.CollectionRecommendation.Benchmarks.Sample_Types;

namespace ConsoleApp1
{
    internal class Program
    {

        private static readonly Random random = new Random();

        private static int[] sampleIntsArray;
        private static string[] sampleStringsArray;
        private static SimplePerson[] sampleSimplePersonsArray;
        private static ComplexPerson[] sampleComplexPersonsArray;
        private static List<int> sampleIntsList;
        private static List<string> sampleStringsList;
        private static List<SimplePerson> sampleSimplePersonsList;
        private static List<ComplexPerson> sampleComplexPersonsList;

        ////private static void Main(string[] args)
        ////{
        ////    sampleIntsArray = Enumerable.Range(0, 15).ToArray();
        ////    sampleStringsArray = Enumerable.Range(0, 15).Select(_ => PersonInstanceCreator.GenerateRandomStringBasedOnLength(15)).ToArray();
        ////    sampleSimplePersonsArray = PersonInstanceCreator.GenerateRandomSimplePersons(15).ToArray();
        ////    sampleComplexPersonsArray = PersonInstanceCreator.GenerateRandomComplexPersons(15).ToArray();

        ////    sampleIntsList = Enumerable.Range(0, 15).ToList();
        ////    sampleStringsList = Enumerable.Range(0, 15).Select(_ => PersonInstanceCreator.GenerateRandomStringBasedOnLength(15)).ToList();
        ////    sampleSimplePersonsList = PersonInstanceCreator.GenerateRandomSimplePersons(15);
        ////    sampleComplexPersonsList = PersonInstanceCreator.GenerateRandomComplexPersons(15);
        ////}
        
        private static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}