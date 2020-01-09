using System;
using System.Collections.Generic;
using BenchmarkDotNet.Running;
using Robotmaster.CollectionRecommendation.Performance.SampleTypes;

namespace Robotmaster.CollectionRecommendation.Performance
{
    internal class Program
    {
        private static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}