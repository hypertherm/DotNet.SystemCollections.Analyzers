using System;
using BenchmarkDotNet.Running;

namespace Robotmaster.CollectionRecommendation.Benchmarks
{
    internal class Program
    {
        private static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
