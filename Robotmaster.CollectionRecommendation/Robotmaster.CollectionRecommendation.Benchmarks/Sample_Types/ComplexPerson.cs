using System;
using System.Collections.Generic;
using Robotmaster.CollectionRecommendation.Benchmarks.Sample_Types;

namespace Robotmaster.CollectionRecommendation.Benchmarks
{
    internal class ComplexPerson
    {
        internal string FirstName { get; }

        internal string LastName { get; }

        internal float Weight { get; }

        internal Address HomeAddress { get; }

        internal DateTime Birthday { get; }

        internal string Profession { get; }

        internal string PhoneNumber { get; }

        internal Citizenship Citizenship { get; }

        internal Gender Gender { get; }

        internal IEnumerable<string> Interests { get; }
    }
}