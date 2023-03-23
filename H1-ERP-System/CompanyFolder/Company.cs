using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.CompanyFolder
{
    public enum Currency { DKK, USD, EUR}
    public class Company
    {
        public string CompanyName { get; set; } //Firmanavn
        public string Street { get; set; } //Vej
        public string StreetNumber { get; set; } //Husnummer
        public string PostalCode { get; set; } //Postnummer
        public string City { get; set; } //By
        public string Country { get; set; } //Land
        public Currency Currency { get; set; } //Valuta

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
