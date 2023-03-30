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
    /// CompanyInfoScreen - Displays all the companies with ID and Name. 
    /// Thereafter asks the user which company they would like more information on, like Company name, Address and Currency.
    /// *IS USED TO DISPLAY INFORMATION ON ONE COMPANY*
    /// </summary>
    public class CompanyInfoScreen : Screen
    {
        public override string Title { get; set; } = " Companies ";
        List<Company> l = Database.GetCompanies("SELECT * FROM dbo.Companies");
        protected override void Draw()
        {
            Clear(this);
            ListPage<Company> companyList = new ListPage<Company>();
            for (int i = 0; i < l.Count; i++)
            {
                companyList.Add(l[i]);
            }
            companyList.AddColumn("ID", "Id", 5);
            companyList.AddColumn("Company Name", "CompanyName");
            companyList.Draw();


            Text();
        }

        /// <summary>
        /// Text() - Asks the user which Company they would like *ALL* information on: Company name, Address and Currency used. 
        /// </summary>
        public void Text()
        {
            Console.WriteLine("\nWhich company would you like information about? \nPlease select an ID!");
            Console.Write("> ");
            int.TryParse(Console.ReadLine(), out int choice);
            if (choice != 0 && choice <= l.Count)
            {
                Company cp = l[choice - 1];
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

                Commands(Console.ReadKey(), cp);

                TryAgain();
            }
        }

        public void Commands(ConsoleKeyInfo key, Company? cp)
        {
            if (key.Key == ConsoleKey.F1)
                Draw();
            else if (key.Key == ConsoleKey.F2)
                EditCompany(cp);
            //Something
        }

        public void EditCompany(Company cp)
        {

        }

        /// <summary>
        /// TryAgain() - Asks user if they'd like to try to search for another Company, if not, clears and opens the StartUp page again. 
        /// If yes, goes back to Text().
        /// </summary>
        public void TryAgain()
        {
            Console.WriteLine("\nWould you like to try again?");
            Console.WriteLine("1. Yes \n2. No");
            Console.Write("> ");
            int.TryParse(Console.ReadLine(), out int choice);
            if (choice == 1)
            {
                Clear(this);
                Draw();
                Text();
            }
            else
            {
                Console.Clear();
                StartPage.StartUp();
            }
        }
    }
}
