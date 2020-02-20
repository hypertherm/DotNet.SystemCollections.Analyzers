namespace DotNet.SystemCollections.Analyzers.Performance.SampleTypes
{
    using System;

    public class SimplePerson
    {
        internal string FirstName { get; }

        internal string LastName { get; }

        internal DateTime Birthday { get; }

        public SimplePerson(string firstName, string lastName, DateTime birthday)
        {
            this.FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
            this.LastName = !string.IsNullOrEmpty(lastName) ? lastName: throw new ArgumentNullException(nameof(lastName));
            this.Birthday = birthday != DateTime.MaxValue || birthday != DateTime.MinValue ? birthday : throw new ArgumentException(nameof(birthday));
        }
    }
}
