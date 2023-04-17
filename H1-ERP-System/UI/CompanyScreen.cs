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

            companyList.AddColumn("Company Name", "CompanyName", 20);
            companyList.AddColumn("Country", "Country");
            companyList.AddColumn("Currency", "Currency");
            companyList.AddKey(ConsoleKey.F1, CompanyEditScreen.EditCompany);
            companyList.AddKey(ConsoleKey.F2, CompanyEditScreen.CreateCompany);
            companyList.AddKey(ConsoleKey.F5, CompanyEditScreen.DeleteCompany);



            //companyList.Draw();
            Console.WriteLine("F1  | Edit highlighted\n" +
                              "F2  | Create new\n" +
                              "F5  | Delete\n" +
                              "ESC | Go back");
            Company selected = companyList.Select();
            
            ReturnToStart();
        }
        public static void ReturnToStart()
        {
            //Console.WriteLine("Press any key to go back...");
            //Console.ReadKey();
            StartPage.StartUp();
        }
    }

}
