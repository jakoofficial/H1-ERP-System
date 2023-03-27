using H1_ERP_System.CompanyFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class CompanyInfoScreen : Screen
    {
        public override string Title { get; set; } = "Company Screen";
        protected override void Draw()
        {
            Clear(this);

        }
    }
}
