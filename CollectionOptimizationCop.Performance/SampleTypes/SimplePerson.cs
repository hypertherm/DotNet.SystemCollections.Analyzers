namespace CollectionOptimizationCop.Performance.SampleTypes
{
    using System;

    /// <summary>
    ///     This class is used to represent a simplified person.
    /// </summary>
    public class SimplePerson
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SimplePerson"/> class.
        /// </summary>
        /// <param name="firstName">
        ///     The person's first name.
        /// </param>
        /// <param name="lastName">
        ///     The person's last name.
        /// </param>
        /// <param name="birthday">
        ///     The person's birthday.
        /// </param>
        public SimplePerson(string firstName, string lastName, DateTime birthday)
        {
            this.FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
            this.LastName = !string.IsNullOrEmpty(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
            this.Birthday = birthday != DateTime.MaxValue || birthday != DateTime.MinValue ? birthday : throw new ArgumentException(nameof(birthday));
        }

        /// <summary>
        ///     Gets this person's first name.
        /// </summary>
        internal string FirstName { get; }

        /// <summary>
        ///     Gets this person's last name.
        /// </summary>
        internal string LastName { get; }

        /// <summary>
        ///     Gets this person's birthday.
        /// </summary>
        internal DateTime Birthday { get; }
    }
}
