using H1_ERP_System.CompanyFolder;
using H1_ERP_System.CustomerFolder;
using H1_ERP_System.SalesFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class SalesScreen : Screen
    {
        public override string Title { get; set; } = "Sales";
        List<SalesOrderHeader> orders = Database.GetSalesOrders("ooga");
        protected override void Draw()
        {
            Clear(this);

            ListPage<Customer> lpCustomer = new ListPage<Customer>();
            List<Customer> cList = Database.GetCustomerList();
            for (int i = 0; i < cList.Count; i++)
            {
                lpCustomer.Add(cList[i]);
            }

            lpCustomer.AddColumn("Customer ID", "CustomerId");
            lpCustomer.AddColumn("Customer ID", "PhoneNumber");
            lpCustomer.AddColumn("Customer ID", "Email");
            //lpCustomer.AddColumn("Customer ID", "LastName"); ADD FULLNAME!!!




            ListPage<SalesOrderHeader> salesList = new ListPage<SalesOrderHeader>();
            List<SalesOrderHeader> orders = Database.GetSalesOrders(" ");

            for (int i = 0; i < orders.Count; i++)
            {
                salesList.Add(orders[i]);
            }

            salesList.AddColumn("Sale ID", "CompanyName");
            salesList.AddColumn("Date", "Country");
            salesList.AddColumn("Customer ID", "Currency");
            salesList.AddColumn("Customer Name", "Currency");
            salesList.AddColumn("Total", "Currency");

            salesList.Draw();
            ReturnToStart();
        }
        public void ReturnToStart()
        {
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
            StartPage.StartUp();
        }
    }
}
