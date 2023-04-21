using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class MainMenu : Screen
    {
        public override string Title { get; set; } = "LNE Security A/S";
        protected override void Draw()
        {
            Menu menu = new Menu();
            menu.Add(new CompanyScreen());
            menu.Add(new CustomerScreen());
            menu.Add(new ProductScreen());
            menu.Add(new SalesScreen());

            menu.Start(this);
        }
    }
}
