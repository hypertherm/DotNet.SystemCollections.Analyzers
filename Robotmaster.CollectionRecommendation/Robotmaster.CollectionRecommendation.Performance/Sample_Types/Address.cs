﻿using System;
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
            StreetAddress = !string.IsNullOrEmpty(streetName) ? streetName : "Some street name 8888";
            PostalCode = !string.IsNullOrEmpty(postalCode) ? postalCode : "012 345";
        }
    }
}