using H1_ERP_System.CustomerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.CompanyFolder
{
    public class Company : Address
    {
        public int CompanyId { get; set; }
        public enum Currencies { DKK, USD, EUR }
        public string CompanyName { get; set; } //Firmanavn
        //public Address Address { get; set; }
        //public string Country { get; private set; }
        //public string City { get; private set; }
        //public string PostalCode { get; private set; }
        //public string StreetNumber { get; private set; }
        //public string Street { get; private set; }
        public Currencies Currency { get; set; } //Valuta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="address"></param>
        /// <param name="currency"> Choose between DKK, USD or EUR. </param>
        public Company(int id, string companyName, Currencies currency, string street, string streetNumber, string postalCode, string city, string country) : base(id, street, streetNumber, postalCode, city, country)
        {
            CompanyId = id;
            CompanyName = companyName;
            Currency = currency;
        }



        //public Company(int id, string companyName, Address address, Currencies currency)
        //{
        //    Id = id;
        //    CompanyName = companyName;
        //    Address = address;
        //    Country = address.Country;
        //    City = address.City;
        //    PostalCode = address.PostalCode;
        //    StreetNumber = address.StreetNumber;
        //    Street = address.Street;
        //    Currency = currency;
        //}
    }
}
