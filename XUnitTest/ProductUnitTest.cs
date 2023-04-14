using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP_System;
using H1_ERP_System.ProductFolder;
using static H1_ERP_System.ProductFolder.Product;

namespace XUnitTest
{
    public class ProductUnitTest
    {
        /// <summary>
        /// Creates Test Product to check if Profit is not Null and if it is the right answer.
        /// </summary>
        [Fact]
        public void TestCalculateProfit()
        {
            Product TestProduct = new Product(1, "Test", "test for XUnitTest", 200, 150, 10, Units.Kilogram);
            Assert.Equal(50, TestProduct.CalculateProfit());
        }
    }
}
