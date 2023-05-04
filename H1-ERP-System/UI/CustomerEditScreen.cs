﻿using H1_ERP_System.CompanyFolder;
using H1_ERP_System.CustomerFolder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class CustomerEditScreen : Screen
    {
        public override string Title { get; set; } = "Edit Customer";
        public Customer? Co { get; set; }

        public CustomerEditScreen(Customer co = null)
        {
            Co = co;
        }

        protected override void Draw()
        {
            Clear(this);
        }

        #region CreateCustomer
        /// <summary>
        /// - Console.WriteLine have a s to fix a error in TECHCOOL where it will remove the first letter.
        /// </summary>
        public static void CreateCustomer(Customer co)
        {

            Console.Clear();
            Form<Customer> coEdit = new Form<Customer>();
            co = new Customer();

            coEdit.TextBox($"First Name ", "FirstName");
            coEdit.TextBox("Last Name", "LastName");
            coEdit.TextBox("Phonenumber", "PhoneNumber");
            coEdit.TextBox("Street", "Street");
            coEdit.TextBox("Street Number", "StreetNumber");
            coEdit.TextBox("PostalCode", "PostalCode");
            coEdit.TextBox("City", "City");
            coEdit.TextBox("Country", "Country");
            coEdit.TextBox("EMail", "Email");
            coEdit.TextBox("Last Purchase", "LastPurchase");

        createCustomer:
            Console.WriteLine("Press ESC When Done\n");
            coEdit.Edit(co);
            Customer customer = new Customer(0, co.LastPurchase, co.FirstName, co.LastName, co.PhoneNumber, co.Email, 0, co.Street, co.StreetNumber, co.PostalCode, co.City, co.Country);

            if (!Checker.ChecksIfEmpty(customer))
            {
                Database.AddCustomerToDB(customer);
                Console.Clear();
                Console.WriteLine("s\n Successfully Created");
                Console.ReadLine();
                Screen.Display(new CustomerScreen());
            }
            else
            {
                if (Checker.Retry())
                    goto createCustomer;
            }
        }
        #endregion

        #region EditCustomer
        /// <summary>
        /// - Console.WriteLine have a s to fix a error in TECHCOOL where it will remove the first letter.
        /// </summary>
        public static void EditCustomer(Customer co)
        {
            Console.Clear();

            Form<Customer> coEdit = new Form<Customer>();

            coEdit.TextBox($"First Name ", "FirstName");
            coEdit.TextBox("Last Name", "LastName");
            coEdit.TextBox("Street", "Street");
            coEdit.TextBox("Street Number", "StreetNumber");
            coEdit.TextBox("PostalCode", "PostalCode");
            coEdit.TextBox("City", "City");
            coEdit.TextBox("Phonenumber", "PhoneNumber");
            coEdit.TextBox("EMail", "Email");

        editCustomer:
            Console.WriteLine("Press ESC When Done\n");
            coEdit.Edit(co);
            if (!Checker.ChecksIfEmpty(co))
            {
                Database.UpdateCustomer(co);

                Address coAddress = Database.GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {co.AddressId}");
                coAddress.StreetNumber = co.StreetNumber;
                coAddress.Street = co.Street;
                coAddress.City = co.City;
                coAddress.PostalCode = co.PostalCode;
                coAddress.Country = co.Country;

                if (!Checker.ChecksIfEmpty(coAddress))
                {
                    Database.UpdateAddress(coAddress);
                    Console.Clear();
                    Console.WriteLine("s\n Successfully Updated" +
                                      "\n Press Any Key To return to CustomerList.");
                    Console.ReadKey();
                    Screen.Display(new CustomerScreen());
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
        #endregion

        #region DeleteCustomer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cos"></param>
        public static void DeleteCustomerScreen(Customer cos)
        {
            if (Checker.DeleteData(cos.FullName))
            {
                Database.DeleteCustomer(cos);
                Console.Clear();
                Console.WriteLine("Deleting Complete");
                Console.ReadLine();
            }

            Console.Clear();
        }

        public static void MenuOptionColorSetter(ConsoleColor foreColor, ConsoleColor backColor, string text = "")
        {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.WriteLine(text);
        }
        #endregion
    }
}
