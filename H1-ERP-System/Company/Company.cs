﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.Virksomhed
{
    public enum Currency { DKK, USD, EUR}
    public class Company
    {
        public string CompanyName { get; set; } 
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Currency Currency { get; set; }

        public Company(string companyName, string street, string streetNumber, string postalCode, string city, string country, Currency currency)
        {
            CompanyName = companyName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
            Currency = currency;
        }
    }
}
