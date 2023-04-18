using H1_ERP_System.CompanyFolder;
using H1_ERP_System.CustomerFolder;
using MySqlX.XDevAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class CompanyEditScreen : Screen
    {
        public override string Title { get; set; } = " Edit Company ";
        public Company? CP { get; set; }

        public CompanyEditScreen(Company cp = null)
        {
            CP = cp;
        }

        protected override void Draw()
        {
            Clear(this);
            //EditCompany(CP);
        }

        /// <summary>
        /// Allows the creation of a new Company with new Address
        /// </summary>
        /// <param name="cp">Company</param>
        public static void CreateCompany(Company cp)
        {
            Clear();
            Form<Company> cpEditor = new Form<Company>();
            cp = new Company();

            cpEditor.TextBox($"Company Name ", "CompanyName");
            cpEditor.TextBox("Country", "Country");
            cpEditor.TextBox("City", "City");
            cpEditor.TextBox("PostalCode", "PostalCode");
            cpEditor.TextBox("StreetNumber", "StreetNumber");
            cpEditor.TextBox("Street", "Street");
            cpEditor.SelectBox($"Currency", "Currency");
            cpEditor.AddOption($"Currency", "EUR", Company.Currencies.EUR);
            cpEditor.AddOption($"Currency", "USD", Company.Currencies.USD);
            cpEditor.AddOption($"Currency", "DKK", Company.Currencies.DKK);

        createCompany:
            Console.WriteLine("Press ESC When Done\n");
            cpEditor.Edit(cp);
            Company company = new Company(0, cp.CompanyName, cp.Currency, cp.AddressId, cp.Street, cp.StreetNumber, cp.PostalCode, cp.City, cp.Country);

            if (!Checker.ChecksIfEmpty(cp))
            {
                Database.AddCompany(company);
                Clear();
                Console.WriteLine("\n Successfully Created");
                Console.ReadLine();
                Screen.Display(new CompanyScreen());
            }
            else
            {
                Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Company might have an empty value and can not be made.\n\n" +
                                  "Press ENTER to try again\n" +
                                  "Or ESCAPE to quit creation\n");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    Screen.Display(new CompanyScreen());
                else if (key == ConsoleKey.Enter)
                    goto createCompany;
            }
        }

        /// <summary>
        /// Allows the user to edit the company's information including the address 
        /// </summary>
        /// <param name="cp">Company</param>
        public static void EditCompany(Company cp)
        {
            //var props = new Dictionary<string, object>();
            Clear();

        editCompany:
            Form<Company> cpEditor = new Form<Company>();

            cpEditor.TextBox($"Company Name ", "CompanyName");
            cpEditor.TextBox("Country", "Country");
            cpEditor.TextBox("City", "City");
            cpEditor.TextBox("PostalCode", "PostalCode");
            cpEditor.TextBox("StreetNumber", "StreetNumber");
            cpEditor.TextBox("Street", "Street");
            cpEditor.SelectBox($"Currency", "Currency");
            cpEditor.AddOption($"Currency", "EUR", Company.Currencies.EUR);
            cpEditor.AddOption($"Currency", "USD", Company.Currencies.USD);
            cpEditor.AddOption($"Currency", "DKK", Company.Currencies.DKK);

            Console.WriteLine("Press ESC When Done\n");
            cpEditor.Edit(cp);
            if (!Checker.ChecksIfEmpty(cp))
            {
                Database.UpdateCompany(cp);

                Address adr = Database.GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {cp.AddressId}");
                adr.StreetNumber = cp.StreetNumber;
                adr.Street = cp.Street;
                adr.City = cp.City;
                adr.PostalCode = cp.PostalCode;
                adr.Country = cp.Country;

                if (!Checker.ChecksIfEmpty(adr))
                {
                    Database.UpdateAddress(adr);
                    Clear();
                    Console.WriteLine("\n Successfully Updated");
                    Console.ReadLine();
                    Screen.Display(new CompanyScreen());
                }
                else
                {
                    Console.WriteLine("Address might have an empty value and can not be updated.\n\n" +
                                      "Press ENTER to try again\n" +
                                      "Or ESCAPE to quit editing\n");
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Escape)
                        Screen.Display(new CompanyScreen());
                    else if (key == ConsoleKey.Enter)
                        goto editCompany;
                }
            }
            else
            {
                Clear();
                Console.WriteLine("Company might have an empty value and can not be updated.\n\n" +
                                  "Press ENTER to try again\n" +
                                  "Or ESCAPE to quit editing\n");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    Screen.Display(new CompanyScreen());
                else if (key == ConsoleKey.Enter)
                    goto editCompany;
            }
        }


        /// <summary>
        /// deletes a specified Company if the user confrims it 
        /// </summary>
        /// <param name="comp"></param>
        public static void DeleteCompany(Company comp)
        {
            repeat:
            Clear();
            Console.WriteLine($"Are you sure you want to delete '" +
                $"{comp.CompanyName}' \n1. yes\n2. No");
            int.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case 1:
                    Database.RemoveCompany(comp);
                    Clear();
                    Console.WriteLine($"The company {comp.CompanyName} has been deleted\nPres enter to return");
                    Console.ReadLine();
                    break;

                case 2:
                    Clear();
                    Console.WriteLine($"The deletion of {comp.CompanyName} has been canceled\nPres enter to return");
                    Console.ReadLine();
                    break;

                default:
                    goto repeat;

            }

            
            Screen.Display(new CompanyScreen());
         
        }
    }
}
