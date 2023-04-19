using H1_ERP_System.CompanyFolder;
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
        public override string Title { get; set; } = "Customer List";

        #region CustomerScreen
        protected override void Draw()
        {
            Title = "Customerlist";
            Clear(this);
            ListPage<Customer> customerListPage = new ListPage<Customer>();
            List<Customer> list = Database.GetCustomerList();
            foreach (Customer customer in list)
                customerListPage.Add(customer);

            customerListPage.AddColumn("Customers ID", "CustomerId");
            customerListPage.AddColumn("Name", "FullName");
            customerListPage.AddColumn("PhoneNumber", "PhoneNumber");
            customerListPage.AddColumn("Email", "Email");
            customerListPage.AddKey(ConsoleKey.F2, CustomerEditScreen.CreateCustomer);
            
            
            Console.WriteLine("Enter  | Select\n" +
                              "F2     | Create new\n" +
                              "ESC    | Go back");

            Customer selected = customerListPage.Select();

            if (selected != null)
            {
                Title = $"{selected.FirstName} {selected.LastName}";
                Clear(this);
                CustomerDetails(selected);
            }
            else
                Clear();
                Quit();
        }
        #endregion

        #region CustomerDetails
        public void CustomerDetails(Customer selected)
        {
            ListPage<Customer> selectedCustomerListPage = new ListPage<Customer>();
            selectedCustomerListPage.Add(selected);

            selectedCustomerListPage.AddColumn("Name", "FullName");
            selectedCustomerListPage.AddColumn("Street", "Street");
            selectedCustomerListPage.AddColumn("Street number", "StreetNumber");
            selectedCustomerListPage.AddColumn("PostalCode", "PostalCode");
            selectedCustomerListPage.AddColumn("City", "City");
            selectedCustomerListPage.AddColumn("Country", "Country");
            selectedCustomerListPage.AddColumn("Last purchase", "LastPurchase");
            selectedCustomerListPage.AddKey(ConsoleKey.F2, CustomerEditScreen.EditCustomer);

            Console.WriteLine("F2  | Edit highlighted\n" +
                              "ESC | Go back");
            Customer selectedCustomer = selectedCustomerListPage.Select();

            Clear();
            Quit();
        }
        #endregion

    }
}
