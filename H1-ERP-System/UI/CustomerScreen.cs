using H1_ERP_System.CustomerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class CustomerScreen : Screen
    {
        public override string Title { get; set; } = "Customerlist";
        protected override void Draw()
        {
            Clear(this);
            ListPage<Customer> customerListPage = new ListPage<Customer>();
            List<Customer> list = Database.GetCustomerList();
            foreach (Customer customer in list)
            {
                customerListPage.Add(customer);
            }

            customerListPage.AddColumn("Customers ID", "CustomerId");
            customerListPage.AddColumn("Last purchase", "LastPurchase");
            customerListPage.AddColumn("Firstname", "FirstName");
            customerListPage.AddColumn("LastName", "LastName");
            customerListPage.AddColumn("Address", "Address");
            customerListPage.AddColumn("PhoneNumber", "PhoneNumber");
            customerListPage.AddColumn("Email", "Email");

            customerListPage.Select();
            //if (selected != null)
            //{
            //    Console.WriteLine($"You selected {selected.FirstName} {selected.LastName}");
            //    Clear(this);
            //}
        }
    }
}
