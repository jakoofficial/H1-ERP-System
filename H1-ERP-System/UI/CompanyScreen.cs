using H1_ERP_System.CompanyFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class CompanyScreen : Screen
    {
        public override string Title { get; set; } = "Displaying Companies";

        //Displays a table, with the Companies' information. [Name, Country and Which Currency they use.]
        protected override void Draw()
        {
            Clear(this);

            List<Company> l = Database.GetCompany("SELECT * FROM dbo.Companies");
            ListPage<Company> addPage = new ListPage<Company>();
            for (int i = 0; i < l.Count; i++)
            {
                addPage.Add(l[i]);
            }

            addPage.AddColumn("Company Name", "CompanyName");
            addPage.AddColumn("Country", "Country");
            addPage.AddColumn("Currency", "Currency");

            addPage.Draw();
        }
    }

}
