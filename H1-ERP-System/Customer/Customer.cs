using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.Customer
{
    public class Customer : Person
    {
        public int CustomerId { get; private set; } //Bruger identifikation
        public DateTime LastPurchase { get; set; } //Sidste købte dato

        /// <summary>
        /// For when a customer needs to be created
        /// </summary>
        /// <param name="customerId">Int : The ID of the customer</param>
        /// <param name="lastPurchase">DateTime : The date of last purchase</param>
        /// <param name="firstName">String : First name of the customer</param>
        /// <param name="lastName">String : Last name of the customer</param>
        /// <param name="address">String : The address of the customer</param>
        /// <param name="phoneNumber">String : The phone number of the customer</param>
        /// <param name="email">String : The customer's E-mail</param>
        public Customer(int customerId, DateTime lastPurchase, string firstName, string lastName, Address address, string phoneNumber, string email) 
            : base(firstName, lastName, address, phoneNumber, email)
        {
            CustomerId = customerId;
            LastPurchase = lastPurchase;
        }
    }
}
