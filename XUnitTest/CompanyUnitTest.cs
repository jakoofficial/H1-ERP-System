using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUnitTest;
using H1_ERP_System;
using H1_ERP_System.CompanyFolder;

namespace XUnitTest
{
    public class CompanyUnitTest
    {
        [Fact]
        public void TestIfEmpty()
        {
            Company companyEmp = new Company(0,"", 0, 0, "", "", "", "", "");
            Assert.True(Checker.IfEmpty(companyEmp));

        }
        [Fact]
        public void TestIfFull()
        {
            Company companyFull = new Company(0,"asd", 0, 0, "ad", "asd", "ad", "asd", "ad");
            Assert.True(!Checker.IfEmpty(companyFull));

        }
    }
}
