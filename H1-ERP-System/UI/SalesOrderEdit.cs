﻿using H1_ERP_System.CustomerFolder;
using H1_ERP_System.SalesFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class SalesOrderEdit : Screen
    {
        public override string Title { get; set; } = "Sales Order Edit/Create";
        protected override void Draw()
        {
            Clear(this);
        }

        public static void EditSale(SaleOrderLine sl)
        {
            Clear();

            List<SaleOrderHeader> saleHeaderList = Database.GetSaleOrders($"SELECT * FROM dbo.SalesOrders WHERE OrderNumber = {sl.SalesOrderHeaderID}");

            Customer customer = saleHeaderList[0].CustomerId;

            Form<Customer> customerForm = new Form<Customer>();

            customerForm.TextBox($"First Name ", "FirstName");
            customerForm.TextBox("Last Name", "LastName");
            customerForm.TextBox("Street", "Street");
            customerForm.TextBox("Street Number", "StreetNumber");
            customerForm.TextBox("PostalCode", "PostalCode");
            customerForm.TextBox("City", "City");
            customerForm.TextBox("Phonenumber", "PhoneNumber");
            customerForm.TextBox("EMail", "Email");

            editCustomer:
            Console.WriteLine("Press ESC When Done\n");
            customerForm.Edit(customer);

            if (!Checker.ChecksIfEmpty(customer))
            {
                Database.UpdateCustomer(customer);

                Address customerAddress = Database.GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {customer.AddressId}");
                customerAddress.StreetNumber = customer.StreetNumber;
                customerAddress.Street = customer.Street;
                customerAddress.City = customer.City;
                customerAddress.PostalCode = customer.PostalCode;
                customerAddress.Country = customer.Country;

                if (!Checker.ChecksIfEmpty(customerAddress))
                {
                    Database.UpdateAddress(customerAddress);
                    Console.Clear();
                    Console.WriteLine("Successfully Updated");
                    Console.ReadLine();
                    Screen.Display(new CustomerScreen());
                }
                else
                {
                    Console.WriteLine("Address might have an empty value and can not be updated.\n\n" +
                                      "Press ENTER for trying again\n" +
                                      "Or ESCAPE to quit editing\n");
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Escape)
                        Screen.Display(new CustomerScreen());
                    else if (key == ConsoleKey.Enter)
                        goto editCustomer;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Customer might have an empty value and can not be updated.\n\n" +
                                  "Press ENTER for trying again\n" +
                                  "Or ESCAPE to quit editing\n");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    Screen.Display(new CustomerScreen());
                else if (key == ConsoleKey.Enter)
                    goto editCustomer;
            }
        }
    }
}
