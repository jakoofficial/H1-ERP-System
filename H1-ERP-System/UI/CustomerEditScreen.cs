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
