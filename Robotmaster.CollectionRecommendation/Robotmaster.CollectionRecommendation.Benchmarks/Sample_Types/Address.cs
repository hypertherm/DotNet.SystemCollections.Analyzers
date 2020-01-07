using System;
using System.Collections.Generic;
using System.Text;

namespace Robotmaster.CollectionRecommendation.Benchmarks.Sample_Types
{
    using System;

    public class Address
    {
        public string StreetAddress { get; }
        public string PostalCode { get; }

        public Address(string streetName, string postalCode)
        {
            StreetAddress = !string.IsNullOrEmpty(streetName) ? streetName : throw new ArgumentNullException(nameof(streetName));
            PostalCode = !string.IsNullOrEmpty(postalCode) ? postalCode : throw new ArgumentNullException(nameof(postalCode));
        }
    }
}
