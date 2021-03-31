namespace CollectionOptimizationCop.Performance.SampleTypes
{
    /// <summary>
    ///     This represents an address.
    /// </summary>
    public class Address
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="streetName">
        ///     The street name of the address.
        /// </param>
        /// <param name="postalCode">
        ///     The postal code of the address.
        /// </param>
        public Address(string streetName, string postalCode)
        {
            this.StreetAddress = !string.IsNullOrEmpty(streetName) ? streetName : "Some street name 8888";
            this.PostalCode = !string.IsNullOrEmpty(postalCode) ? postalCode : "012 345";
        }

        /// <summary>
        ///     Gets the street address.
        /// </summary>
        public string StreetAddress { get; }

        /// <summary>
        ///     Gets the postal code.
        /// </summary>
        public string PostalCode { get; }
    }
}
