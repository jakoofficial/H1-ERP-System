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
            EditCompany(CP);
        }

        /// <summary>
        /// Allows the user to edit the company's information including the address 
        /// </summary>
        /// <param name="cp"></param>
        public static void EditCompany(Company cp)
        {
            //var props = new Dictionary<string, object>();
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

            Database.UpdateCompany(cp);
            
            Address adr = Database.GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {cp.Id}");
            adr.StreetNumber = cp.StreetNumber; 
            adr.Street = cp.Street;
            adr.City = cp.City; 
            adr.PostalCode = cp.PostalCode;
            adr.Country = cp.Country;

            Database.UpdateAddress(adr);

            Clear();
            Console.WriteLine("\n Successfully Updated");
            Console.ReadLine();
            Screen.Display(new CompanyScreen());
        }
    }
}
