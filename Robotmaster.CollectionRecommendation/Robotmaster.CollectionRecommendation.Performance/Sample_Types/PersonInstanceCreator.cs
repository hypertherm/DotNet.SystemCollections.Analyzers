using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robotmaster.CollectionRecommendation.Benchmarks.Sample_Types
{
    internal static class PersonInstanceCreator
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Random = new Random();

        internal static SimplePerson GenerateRandomSimplePerson() => new SimplePerson(
            GenerateRandomStringBasedOnLength(Random.Next(100)), GenerateRandomStringBasedOnLength(Random.Next(100)),
            DateTime.Now);

        internal static ComplexPerson GenerateRandomComplexPerson() => new ComplexPerson
        (
            GenerateRandomStringBasedOnLength(Random.Next(100)),
            GenerateRandomStringBasedOnLength(Random.Next(100)),
            ((float) Random.NextDouble()) * 635.0f,
            new Address(GenerateRandomStringBasedOnLength(30), GenerateRandomStringBasedOnLength(6)),
            DateTime.Now,
            GenerateRandomStringBasedOnLength(20),
            GenerateRandomStringBasedOnLength(20),
            (Citizenship)Random.Next(0, 4),
            (Gender)Random.Next(0, 3),
            Enumerable.Range(0, 50).Select(_ => GenerateRandomStringBasedOnLength(50)).ToList()
        );

        internal static List<SimplePerson> GenerateRandomSimplePersons(int peopleCount) => Enumerable.Range(0, peopleCount).Select(_ => GenerateRandomSimplePerson()).ToList();

        internal static List<ComplexPerson> GenerateRandomComplexPersons(int peopleCount) => Enumerable.Range(0, peopleCount).Select(_ => GenerateRandomComplexPerson()).ToList();

        internal static string GenerateRandomStringBasedOnLength(int length) => new string(Enumerable.Repeat(Chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}