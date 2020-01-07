using System;
using System.Collections.Generic;
using System.Text;

namespace Robotmaster.CollectionRecommendation.Benchmarks.Sample_Types
{
    public class SimplePerson
    {
        internal string FirstName { get; }

        internal string LastName { get; }

        internal DateTime Birthday { get; }

        public SimplePerson(string firstName, string lastName, DateTime birthday)
        {
            FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
            LastName = !string.IsNullOrEmpty(lastName) ? lastName: throw new ArgumentNullException(nameof(lastName));
            Birthday = birthday != DateTime.MaxValue || birthday != DateTime.MinValue ? birthday : throw new ArgumentException(nameof(birthday));
        }
    }
}
