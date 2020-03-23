namespace DotNet.SystemCollections.Analyzers.Performance
{
    using BenchmarkDotNet.Running;

    /// <summary>
    ///     This class represents the entry point to the Benchmark.NET runner.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}