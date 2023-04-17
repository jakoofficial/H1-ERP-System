using H1_ERP_System.CompanyFolder;
using H1_ERP_System.CustomerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Creates a customer and checks if any of the values are empty.
        /// - Console.WriteLine have a s to fix a error in TECHCOOL where it will remove the first letter.
        /// </summary>
        /// <param name="co"> Custoemr </param>
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
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("s\n Customer might have an empty value and can not be made.\n\n" +
                                  " Press ENTER for trying again\n" +
                                  " Or ESCAPE to quit creation\n");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    Screen.Display(new CustomerScreen());
                else if (key == ConsoleKey.Enter)
                    goto createCustomer;
            }
        }
        #endregion


        #region EditCustomer
        /// <summary>
        /// Edits a Customer and checks if anything is empty
        /// - Console.WriteLine have a s to fix a error in TECHCOOL where it will remove the first letter.
        /// </summary>
        /// <param name="co"> Customer</param>
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
                    Console.WriteLine("s\n Address might have an empty value and can not be updated.\n\n" +
                                      " Press ENTER for trying again\n" +
                                      " Or ESCAPE to quit editing\n");
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
                Console.WriteLine("s\n Customer might have an empty value and can not be updated.\n\n" +
                                  " Press ENTER for trying again\n" +
                                  " Or ESCAPE to quit editing\n");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    Screen.Display(new CustomerScreen());
                else if (key == ConsoleKey.Enter)
                    goto editCustomer;
            }
        }
        #endregion
    }
}
