﻿using H1_ERP_System.CompanyFolder;
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
    public class SalesEditScreen : Screen
    {
        public override string Title { get; set; } = "Sales Order Edit/Create";
        protected override void Draw()
        {
            Clear(this);
        }

        public static void CreateSale(SaleOrderHeader? s)
        {
            //Customer + Orderheader needs to be made
            Clear();
            Customer c = new Customer();

            Form<Customer> newCustomerForm = new Form<Customer>();
            newCustomerForm.TextBox($"First Name ", "FirstName");
            newCustomerForm.TextBox("Last Name", "LastName");
            newCustomerForm.TextBox("Phonenumber", "PhoneNumber");
            newCustomerForm.TextBox("Street", "Street");
            newCustomerForm.TextBox("Street Number", "StreetNumber");
            newCustomerForm.TextBox("PostalCode", "PostalCode");
            newCustomerForm.TextBox("City", "City");
            newCustomerForm.TextBox("Country", "Country");
            newCustomerForm.TextBox("EMail", "Email");
            newCustomerForm.TextBox("Last Purchase", "LastPurchase");

        newCustomerForm:
            Console.WriteLine("Press ESC When Done\n");
            newCustomerForm.Edit(c);

            Customer customer = new Customer(0, c.LastPurchase, c.FirstName, c.LastName, c.PhoneNumber, c.Email, c.AddressId, c.Street, c.StreetNumber, c.PostalCode, c.City, c.Country);
            if (!Checker.IfEmpty(customer))
            {
                Database.AddCustomerToDB(customer);
                Customer gotCustomer = Database.GetCustomer("SELECT TOP(1) * FROM dbo.Customers ORDER BY CustomerId DESC");
                SaleOrderHeader slh = new SaleOrderHeader(0,
                    DateTime.Now.ToString(), DateTime.Now.ToString(), gotCustomer, OrderStage.None, new List<SaleOrderLine>());
                if (slh != null)
                {
                    Database.AddSaleOrderToDB(slh);
                    Console.Clear();
                    Console.WriteLine("SSuccessfully Created");
                    Console.ReadKey();
                    Screen.Display(new SalesScreen());
                }
                else
                {
                    if (Checker.Retry())
                        goto newCustomerForm;
                }
            }
            else
            {
                if (Checker.Retry())
                    goto newCustomerForm;
            }
        }

        public static void EditSale(SaleOrderLine sl)
        {
            Clear();

            List<SaleOrderHeader> saleHeaderList = Database.GetSaleOrders($"SELECT * FROM dbo.SaleOrders WHERE OrderNumber = {sl.SalesOrderHeaderID}");

            Customer customer = saleHeaderList[0].CustomerId;

            Form<Customer> customerForm = new Form<Customer>();

            customerForm.TextBox($"First Name ", "FirstName");
            customerForm.TextBox("Last Name", "LastName");
            customerForm.TextBox("Phonenumber", "PhoneNumber");
            customerForm.TextBox("Street", "Street");
            customerForm.TextBox("Street Number", "StreetNumber");
            customerForm.TextBox("PostalCode", "PostalCode");
            customerForm.TextBox("City", "City");
            customerForm.TextBox("Country", "Country");
            customerForm.TextBox("Email", "Email");
            customerForm.TextBox("Last Purchase", "LastPurchase");

        editCustomer:
            Console.WriteLine("Press ESC When Done\n");
            customerForm.Edit(customer);

            if (!Checker.IfEmpty(customer))
            {
                Database.UpdateCustomer(customer);

                Address customerAddress = Database.GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {customer.AddressId}");
                customerAddress.StreetNumber = customer.StreetNumber;
                customerAddress.Street = customer.Street;
                customerAddress.City = customer.City;
                customerAddress.PostalCode = customer.PostalCode;
                customerAddress.Country = customer.Country;

                if (!Checker.IfEmpty(customerAddress))
                {
                    Database.UpdateAddress(customerAddress);
                    Console.Clear();
                    Console.WriteLine("SSuccessfully Updated");
                    Console.ReadKey();
                    Screen.Display(new SalesScreen());
                }
                else
                {
                    if (Checker.Retry())
                        goto editCustomer;
                }
            }
            else
            {
                if (Checker.Retry())
                    goto editCustomer;
            }
        }

        public static void DeleteSale(SaleOrderHeader sale)
        {
            if (Checker.DeleteData("sale: " + Convert.ToString(sale.SaleOrderId)))
            {
                Database.RemoveSaleOrderHeader(sale.SaleOrderId);
                Console.Clear();
                Console.WriteLine("Deletion Completed");
                Console.ReadKey();
            }
            Console.Clear();
        }
    }
}