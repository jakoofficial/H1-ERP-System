using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.Customer
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }

        public Person(string firstName, string lastName, Address address, string phoneNumber, string email) 
        { 
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public string GetName()
        {
            return FirstName+" "+LastName;
        }
    }
}
