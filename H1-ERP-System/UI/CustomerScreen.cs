﻿using H1_ERP_System.CompanyFolder;
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
        public override string Title { get; set; } = "Customers";

        #region CustomerScreen
        protected override void Draw()
        {
            ListPage<Customer> customerListPage = new ListPage<Customer>();
            List<Customer> list = Database.GetCustomerList();
            if (list.Count != 0 ) 
            {
                foreach (Customer customer in list)
                    customerListPage.Add(customer);

                customerListPage.AddColumn("Customers ID", "CustomerId");
                customerListPage.AddColumn("Name", "FullName");
                customerListPage.AddColumn("PhoneNumber", "PhoneNumber");
                customerListPage.AddColumn("Email", "Email");
                customerListPage.AddKey(ConsoleKey.F1, CustomerEditScreen.CreateCustomer);
                customerListPage.AddKey(ConsoleKey.F5, CustomerEditScreen.DeleteCustomerScreen);

                Console.WriteLine("Enter  | Select\n" +
                                  "F1     | Create new\n" +
                                  "F5     | Delete\n" +
                                  "ESC    | Go back");

                Customer selected = customerListPage.Select();

                if (selected != null)
                {
                    Title = $"{selected.FirstName} {selected.LastName}";
                    Clear(this);
                    CustomerDetails(selected);
                }
                else
                    Quit();
            }
            else
            {
                CustomerEditScreen.CreateCustomer(new Customer());
            }
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

            Console.WriteLine("F2  | Edit\n" +
                              "ESC | Go back");
            
            Customer selectedCustomer = selectedCustomerListPage.Select();

            Console.Clear();
            Title = "Customers";
        }
        #endregion
    }
}
