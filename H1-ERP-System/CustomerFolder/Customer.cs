using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.CustomerFolder
{
    public class Customer : Person
    {

        public int CustomerId { get; private set; } //Bruger identifikation
        public string LastPurchase { get; set; } //Sidste købte dato
        public string FullName { get; private set; }


        /// <summary>
        /// For when a customer needs to be created
        /// </summary>
        /// <param name="customerId">The ID of the customer</param>
        /// <param name="lastPurchase">The date of last purchase</param>
        /// <param name="firstName">First name of the customer</param>
        /// <param name="lastName">Last name of the customer</param>
        /// <param name="phoneNumber">The phone number of the customer</param>
        /// <param name="email">The customer's E-mail</param>
        /// <param name="addressId"></param>
        /// <param name="street">The street address</param>
        /// <param name="streetNumber">the street number</param>
        /// <param name="postalCode">Postal code for the address</param>
        /// <param name="city">City where address is located</param>
        /// <param name="country">Country where address is located</param>
        public Customer(int customerId, string lastPurchase, string firstName, string lastName, string phoneNumber, string email, int addressId, string street, string streetNumber, string postalCode, string city, string country) : base(firstName, lastName, phoneNumber, email, addressId, street, streetNumber, postalCode, city, country)
        {
            CustomerId = customerId;
            LastPurchase = lastPurchase;
            FullName = GetName();
        }
        


    }
}
