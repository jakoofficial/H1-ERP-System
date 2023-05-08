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
                companyList.AddKey(ConsoleKey.F1, CompanyEditScreen.EditCompany);
                companyList.AddKey(ConsoleKey.F2, CompanyEditScreen.CreateCompany);
                companyList.AddKey(ConsoleKey.F5, CompanyEditScreen.DeleteCompany);

                //companyList.Draw();
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
            } else
                CompanyEditScreen.CreateCompany(new Company());
        }

        public void Text(Company cp)
        {
            //Console.WriteLine("\nWhich company would you like information about? \nPlease select an ID!");
            //Console.Write("> ");
            //if (choice != 0 && choice <= l.Count)
            //{
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
            //}
        }

        /// <summary>
        /// TryAgain() - Asks user if they'd like to try to search for another Company, if not, clears and opens the StartUp page again. 
        /// If yes, goes back to Text().
        /// </summary>
        public void TryAgain()
        {
            Clear();
            Console.WriteLine("\nWould you like to try again?");
            Console.WriteLine("1. Yes \n2. No");
            Console.Write("> ");
            int.TryParse(Console.ReadLine(), out int choice);
            if (choice == 1)
            {
                Clear(this);
                Draw();
            }
            else
            {
                Console.Clear();
                Quit();
            }
        }
    }
}
