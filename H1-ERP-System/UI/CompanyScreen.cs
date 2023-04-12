using H1_ERP_System.CompanyFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    /// <summary>
    /// CompanyScreen - Displays all the companies with Name, Country and Currency. 
    /// *IS USED TO DISPLAY SIMPLE INFORMATION ON ALL COMPANIES IN THE DATABASE.*
    /// </summary>
    public class CompanyScreen : Screen
    {
        public override string Title { get; set; } = " Displaying Companies ";
        protected override void Draw()
        {
            Clear(this);

            List<Company> l = Database.GetCompanies("SELECT * FROM dbo.Companies");
            ListPage<Company> companyList = new ListPage<Company>();
            for (int i = 0; i < l.Count; i++)
            {
                companyList.Add(l[i]);
            }

            companyList.AddColumn("Company Name", "CompanyName");
            companyList.AddColumn("Country", "Country");
            companyList.AddColumn("Currency", "Currency");
            companyList.AddKey(ConsoleKey.F1, CompanyEditScreen.EditCompany);

            //companyList.Draw();
            Console.WriteLine("Press F2 For ");
                Company selected = companyList.Select();
            if (Console.ReadKey().Key == ConsoleKey.F2)
            {
                Screen.Display(new CompanyEditScreen(selected));
            }
            //CompanyEditScreen.EditCompany(selected);

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
