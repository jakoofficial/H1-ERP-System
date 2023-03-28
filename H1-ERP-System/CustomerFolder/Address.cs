using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.CustomerFolder
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } //Vej
        public string StreetNumber { get; set; } //Vejnummer
        public string PostalCode { get; set; } //Post nummer
        public string City { get; set; } //By
        public string Country { get; set; } //Land

        /// <summary>
        /// For when an address is needed
        /// </summary>
        /// <param name="street">String : The street address</param>
        /// <param name="streetNumber">String : the street number</param>
        /// <param name="postalCode">String : Postal code for the address</param>
        /// <param name="city">String : City where address is located</param>
        /// <param name="country">String : Country where address is located</param>
        public Address(int id, string street, string streetNumber, string postalCode, string city, string country) 
        {
            Id = id;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }
    }
}
