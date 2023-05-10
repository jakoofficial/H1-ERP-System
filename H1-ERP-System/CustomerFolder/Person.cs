using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.CustomerFolder
{
    abstract public class Person : Address
    {
        public string FirstName { get; set; } //Fornavn
        public string LastName { get; set; } //Efternavn
        public string PhoneNumber { get; set; } //Telefon nummer
        public string Email { get; set; } //E-mail

        /// <summary>
        /// Used to create an object of type: person
        /// </summary>
        /// <param name="firstName">The first name of the person</param>
        /// <param name="lastName">The last name of the person</param>
        /// <param name="phoneNumber">The phone number of the person</param>
        /// <param name="email">E-mail the person uses</param>
        /// <param name="addressId"></param>
        /// <param name="street">The street address</param>
        /// <param name="streetNumber">the street number</param>
        /// <param name="postalCode">Postal code for the address</param>
        /// <param name="city">City where address is located</param>
        /// <param name="country">Country where address is located</param>
        protected Person(string firstName, string lastName, string phoneNumber, string email, int addressId, string street, string streetNumber, string postalCode, string city, string country) : base(addressId, street, streetNumber, postalCode, city, country)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        
        /// <summary>
        /// Gets the name of the person, combining first name and last name.
        /// </summary>
        /// <returns>String : Gets the fullname of the person</returns>
        public string GetName()
        {
            return FirstName+" "+LastName;
        }
    }
}
