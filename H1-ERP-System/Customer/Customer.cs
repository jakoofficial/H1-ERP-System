using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.Customer
{
    public class Customer : Person
    {

        public int CustomerId { get; private set; }
        public DateTime LastPurchase { get; set; }
        public Customer(int customerId, DateTime lastPurchase, string firstName, string lastName, Address address, string phoneNumber, string email) 
            : base(firstName, lastName, address, phoneNumber, email)
        {
            CustomerId = customerId;
            LastPurchase = lastPurchase;
        }
    }
}
