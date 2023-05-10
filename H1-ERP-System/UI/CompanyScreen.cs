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
    /// <summary>
    /// CompanyScreen - Displays all the companies with Name, Country and Currency. 
    /// *IS USED TO DISPLAY SIMPLE INFORMATION ON ALL COMPANIES IN THE DATABASE.*
    /// </summary>
    public class CompanyScreen : Screen
    {
        public override string Title { get; set; } = "Companies";
        protected override void Draw()
        {
            Clear(this);

            List<Company> l = Database.GetCompanies("SELECT * FROM dbo.Companies");
            ListPage<Company> companyList = new ListPage<Company>();
            if (l.Count != 0)
            {
                for (int i = 0; i < l.Count; i++)
                    companyList.Add(l[i]);

                companyList.AddColumn("Company Name", "CompanyName", 20);
                companyList.AddColumn("Country", "Country");
                companyList.AddColumn("Currency", "Currency");
                companyList.AddKey(ConsoleKey.F1, CompanyEditScreen.CreateCompany);
                companyList.AddKey(ConsoleKey.F2, CompanyEditScreen.EditCompany);
                companyList.AddKey(ConsoleKey.F5, CompanyEditScreen.DeleteCompany);

                Console.WriteLine("F1  | Create new\n" +
                                  "F2  | Edit highlighted\n" +
                                  "F5  | Delete\n" +
                                  "ESC | Go back");
                Company cp = companyList.Select();

                if (cp != null)
                {
                    Clear(this);
                    Text(cp);
                }
                else
                {
                    Clear();
                    Quit();
                }
            } 
            else
                CompanyEditScreen.CreateCompany(new Company());
        }

        public void Text(Company cp)
        {
            ListPage<Company> companyInfoList = new ListPage<Company>();
            companyInfoList.Add(cp);

            companyInfoList.AddColumn("Company Name", "CompanyName");
            companyInfoList.AddColumn("Street", "Street");
            companyInfoList.AddColumn("Street Number", "StreetNumber");
            companyInfoList.AddColumn("Postal code", "PostalCode");
            companyInfoList.AddColumn("City", "City");
            companyInfoList.AddColumn("Country", "Country");
            companyInfoList.AddColumn("Currency", "Currency");

            companyInfoList.Draw();

            Console.ReadKey();
            Clear(this);
            Quit();
        }
    }
}
