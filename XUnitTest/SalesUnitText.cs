using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP_System;
using H1_ERP_System.CustomerFolder;
using H1_ERP_System.SalesFolder;
using H1_ERP_System.UI;

namespace XUnitTest
{
    public class SalesUnitText
    {
        [Fact]
        public void CustomerIdTest()
        {
            //Customer ID test
            List<SaleOrderLine> sol = new List<SaleOrderLine>();
            Customer c = new Customer(2, "lastPurchase", "f-name", "l-name", "phoneNumber", "email", 1, "street", "", "", "", "");
            Assert.Equal(2, c.CustomerId);
            //OrderNumber test
            SalesOrderHeader soh = new SalesOrderHeader(30, "date", "date", c, OrderStage.Packaged, sol);
            Assert.Equal(30, soh.OrderNumber);
        }
    }
}
