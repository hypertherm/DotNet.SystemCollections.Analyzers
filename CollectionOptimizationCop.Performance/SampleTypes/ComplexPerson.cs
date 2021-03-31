namespace CollectionOptimizationCop.Performance.SampleTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     This is a complex person.
    /// </summary>
    public class ComplexPerson
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ComplexPerson"/> class.
        /// </summary>
        /// <param name="firstName">
        ///     The person's first name.
        /// </param>
        /// <param name="lastName">
        ///     The person's last name.
        /// </param>
        /// <param name="weight">
        ///     The person's weight.
        /// </param>
        /// <param name="address">
        ///     The person's address.
        /// </param>
        /// <param name="birthday">
        ///     The person's birthday.
        /// </param>
        /// <param name="profession">
        ///     The person's profession.
        /// </param>
        /// <param name="phoneNumber">
        ///     The person's phone number.
        /// </param>
        /// <param name="citizenship">
        ///     The person's citizenship.
        /// </param>
        /// <param name="gender">
        ///     The person's gender.
        /// </param>
        /// <param name="interests">
        ///     The person's interests.
        /// </param>
        public ComplexPerson(
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
            this.FirstName = !string.IsNullOrEmpty(firstName) ? firstName : "Some Name";
            this.LastName = !string.IsNullOrEmpty(lastName) ? lastName : "Some Name";
            this.Weight = weight > 0.00 && weight <= 635.00 ? weight : throw new ArgumentException(nameof(weight));
            this.HomeAddress = address ?? throw new ArgumentNullException(nameof(address));
            this.Birthday = birthday != DateTime.MaxValue || birthday != DateTime.MinValue ? birthday : throw new ArgumentException(nameof(birthday));
            this.Profession = !string.IsNullOrEmpty(profession) ? profession : throw new ArgumentNullException(nameof(profession));
            this.PhoneNumber = !string.IsNullOrEmpty(phoneNumber) ? phoneNumber : throw new ArgumentNullException(nameof(phoneNumber));
            this.Citizenship = citizenship;
            this.Gender = gender;
            this.Interests = interests.Count != 0 ? interests : (IList<string>)Enumerable.Empty<string>();
        }

        /// <summary>
        ///     Gets the person's first name.
        /// </summary>
        internal string FirstName { get; }

        /// <summary>
        ///     Gets the person's last name.
        /// </summary>
        internal string LastName { get; }

        /// <summary>
        ///     Gets the person's weight.
        /// </summary>
        internal float Weight { get; }

        /// <summary>
        ///     Gets the person's home address.
        /// </summary>
        internal Address HomeAddress { get; }

        /// <summary>
        ///     Gets the person's birthday.
        /// </summary>
        internal DateTime Birthday { get; }

        /// <summary>
        ///     Gets the person's profession.
        /// </summary>
        internal string Profession { get; }

        /// <summary>
        ///     Gets the person's phone number.
        /// </summary>
        internal string PhoneNumber { get; }

        /// <summary>
        ///     Gets the person's citizenship.
        /// </summary>
        internal Citizenship Citizenship { get; }

        /// <summary>
        ///     Gets the person's gender.
        /// </summary>
        internal Gender Gender { get; }

        /// <summary>
        ///     Gets the person's interests.
        /// </summary>
        internal IList<string> Interests { get; }
    }
}