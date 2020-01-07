using System;
using System.Collections.Generic;
using System.Linq;
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

        internal IList<string> Interests { get; }

        internal ComplexPerson(
            string firstName,
            string lastName,
            float weight,
            Address address,
            DateTime birthday,
            string profession,
            string phoneNumber,
            Citizenship citizenship,
            Gender gender,
            IList<string> interests)
        {
            FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
            LastName = !string.IsNullOrEmpty(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
            Weight = weight > 0.00 && weight <= 635.00 ? weight : throw new ArgumentException(nameof(weight));
            HomeAddress = address ?? throw new ArgumentNullException(nameof(address));
            Birthday = birthday != DateTime.MaxValue || birthday != DateTime.MinValue ? birthday : throw new ArgumentException(nameof(birthday));
            Profession = !string.IsNullOrEmpty(profession) ? profession: throw new ArgumentNullException(nameof(profession));
            PhoneNumber = !string.IsNullOrEmpty(phoneNumber) ? phoneNumber: throw new ArgumentNullException(nameof(phoneNumber));
            Citizenship = citizenship;
            Gender = gender;
            Interests = interests.Count != 0 ? interests : (IList<string>)Enumerable.Empty<string>();
        }
    }
}