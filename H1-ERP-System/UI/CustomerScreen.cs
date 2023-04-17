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
        public override string Title { get; set; } = "Customerlist";
        /// <summary>
        /// Draws a list with all the Customers
        /// </summary>
        protected override void Draw()
        {
            Title = "Customerlist";
            Clear(this);
            ListPage<Customer> customerListPage = new ListPage<Customer>();
            List<Customer> list = Database.GetCustomerList();
            foreach (Customer customer in list)
            {
                customerListPage.Add(customer);
            }

            customerListPage.AddColumn("Customers ID", "CustomerId");
            customerListPage.AddColumn("Name", "FullName");
            customerListPage.AddColumn("PhoneNumber", "PhoneNumber");
            customerListPage.AddColumn("Email", "Email");


            Customer selected = customerListPage.Select();
            if (selected != null)
            {
                Title = $"{selected.FirstName} {selected.LastName}";
                Clear(this);
                CustomerDeatials(selected);
            }
            else 
            {
                Quit();
            }
        }

        /// <summary>
        /// Shows the details for a selected Customer
        /// </summary>
        /// <param name="selected"></param>
        public void CustomerDeatials(Customer selected)
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
            //selectedCustomerListPage.Draw();

            Console.WriteLine("F1  | Edit highlighted\n" +
                              "F2  | Create new\n" +
                              "ESC | Go back");
            Customer selectedCustomer = selectedCustomerListPage.Select();

            Console.ReadKey();
            ReturnToStart();
        }
        public static void ReturnToStart()
        {
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
            StartPage.StartUp();
        }
    }
}
