using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.CustomerFolder
{
    abstract public class Person
    {
        public string FirstName { get; private set; } //Fornavn
        public string LastName { get; private set; } //Eftername
        public Address Address { get; private set; } //Addressen
        public string PhoneNumber { get; private set; } //Telefon nummer
        public string Email { get; private set; } //E-mail

        /// <summary>
        /// Used for creating a person
        /// </summary>
        /// <param name="firstName">String : The first name of the person</param>
        /// <param name="lastName">String : The last name of the person</param>
        /// <param name="address">Address : The area of which the person lives</param>
        /// <param name="phoneNumber">String : The phone number of the person</param>
        /// <param name="email">String : E-mail the person uses</param>
        public Person(string firstName, string lastName, Address address, string phoneNumber, string email) 
        { 
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        /// <summary>
        /// Gets the name of the person
        /// </summary>
        /// <returns>String : Gets the fullname of the person</returns>
        public string GetName()
        {
            return FirstName+" "+LastName;
        }
    }
}
