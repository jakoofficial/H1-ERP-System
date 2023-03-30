using H1_ERP_System.CompanyFolder;
using MySqlX.XDevAPI;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public Company CP { get; set; }

        public CompanyEditScreen(Company cp) 
        { 
            CP = cp;
        }

        protected override void Draw()
        {
            Clear(this);
            EditCompany(CP);
        }

        public static void EditCompany(Company cp)
        {
            //var props = new Dictionary<string, object>();
            Form<Company> cpEditor = new Form<Company>();

            cpEditor.TextBox($"Company Name ", "CompanyName");
            cpEditor.SelectBox($"Currency", "Currency");
            cpEditor.AddOption($"Currency", "EUR", Company.Currencies.EUR);
            cpEditor.AddOption($"Currency", "USD", Company.Currencies.USD);
            cpEditor.AddOption($"Currency", "Dkk", Company.Currencies.DKK);

            Console.WriteLine("Press ESC When Done\n");
            cpEditor.Edit(cp);
            Clear();
            Console.WriteLine("\n Done");
            Console.ReadLine();
            Screen.Display(new CompanyScreen());
        }
    }
}
