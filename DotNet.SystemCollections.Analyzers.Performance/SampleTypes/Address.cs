namespace DotNet.SystemCollections.Analyzers.Performance.SampleTypes
{
    public class Address
    {
        public string StreetAddress { get; }
        public string PostalCode { get; }

        public Address(string streetName, string postalCode)
        {
            this.StreetAddress = !string.IsNullOrEmpty(streetName) ? streetName : "Some street name 8888";
            this.PostalCode = !string.IsNullOrEmpty(postalCode) ? postalCode : "012 345";
        }
    }
}
