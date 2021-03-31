namespace CollectionOptimizationCop.Performance.SampleTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     This factory is used to create one or more person objects.
    /// </summary>
    internal static class PersonInstanceCreator
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Random = new Random();

        /// <summary>
        ///     This generates a new, randomly populated <see cref="SimplePerson"/>.
        /// </summary>
        /// <returns>
        ///     This returns the newly created <see cref="SimplePerson"/>.
        /// </returns>
        internal static SimplePerson GenerateRandomSimplePerson() => new SimplePerson(
            GenerateRandomStringBasedOnLength(Random.Next(100)),
            GenerateRandomStringBasedOnLength(Random.Next(100)),
            DateTime.Now);

        /// <summary>
        ///     This generates a new, randomly populated <see cref="ComplexPerson"/>.
        /// </summary>
        /// <returns>
        ///     This returns a newly created <see cref="ComplexPerson"/>.
        /// </returns>
        internal static ComplexPerson GenerateRandomComplexPerson() => new ComplexPerson(
            GenerateRandomStringBasedOnLength(Random.Next(100)),
            GenerateRandomStringBasedOnLength(Random.Next(100)),
            ((float)Random.NextDouble()) * 635.0f,
            new Address(GenerateRandomStringBasedOnLength(30), GenerateRandomStringBasedOnLength(6)),
            DateTime.Now,
            GenerateRandomStringBasedOnLength(20),
            GenerateRandomStringBasedOnLength(20),
            (Citizenship)Random.Next(0, 4),
            (Gender)Random.Next(0, 3),
            Enumerable.Range(0, 50).Select(_ => GenerateRandomStringBasedOnLength(50)).ToList());

        /// <summary>
        ///     This generates a <see cref="List{T}"/> of randomly populated <see cref="SimplePerson"/>.
        /// </summary>
        /// <param name="peopleCount">
        ///     The number of <see cref="SimplePerson"/>s to create.
        /// </param>
        /// <returns>
        ///     This returns a new <see cref="List{T}"/>, filled with <paramref name="peopleCount"/> number of new, randomly populated <see cref="SimplePerson"/>.
        /// </returns>
        internal static List<SimplePerson> GenerateRandomSimplePersons(int peopleCount) => Enumerable.Range(0, peopleCount).Select(_ => GenerateRandomSimplePerson()).ToList();

        /// <summary>
        ///     This generates a <see cref="List{T}"/> of randomly populated <see cref="ComplexPerson"/>.
        /// </summary>
        /// <param name="peopleCount">
        ///     The number of <see cref="ComplexPerson"/>s to create.
        /// </param>
        /// <returns>
        ///     This returns a new <see cref="List{T}"/>, filled with <paramref name="peopleCount"/> number of new, randomly populated <see cref="ComplexPerson"/>.
        /// </returns>
        internal static List<ComplexPerson> GenerateRandomComplexPersons(int peopleCount) => Enumerable.Range(0, peopleCount).Select(_ => GenerateRandomComplexPerson()).ToList();

        /// <summary>
        ///     This generates a randomly generated string, with its length equal to the given <paramref name="length"/>.
        /// </summary>
        /// <param name="length">
        ///     The desired length for the randomly generated string.
        /// </param>
        /// <returns>
        ///     The new, randomly generated string with length of <paramref name="length"/>.
        /// </returns>
        internal static string GenerateRandomStringBasedOnLength(int length) => new string(Enumerable.Repeat(Chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}