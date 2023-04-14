using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using H1_ERP_System;
using H1_ERP_System.CustomerFolder;

namespace XUnitTest
{
    public class CustomerUnitTest
    {
        [Fact]
        public void TestFullName()
        {
            Customer testCustomer = new Customer(1, "bla", "Peter", "Petersen", "12345678", "blabla@gmail.com", 2, "vej", "2", "9000", "By", "Land");
            //string fullName = testCustomer.FullName;

            Assert.Matches("Peter Petersen", testCustomer.GetName()); 
        }
    }
}
