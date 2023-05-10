using H1_ERP_System.ProductFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using H1_ERP_System;

namespace H1_ERP_System.SalesFolder
{
    public class SaleOrderLine
    {
        public int SaleOrderLineId { get; set; }
        public Product Product { get; set; }
        public string PurchasedDate { get; set; } //Købsdato
        public int PurchasedAmount { get; set; } //Mængde købt
        public int SalesOrderHeaderID { get; set; }
        public string ProductName 
        { 
            get
            {
                return Product == null ? "" : Product.Name;
            } 
        }

        public SaleOrderLine(int id, Product product, string purchasedDate, int purchasedAmount, int salesOrderHeaderID)
        {     
            SaleOrderLineId = id;
            Product = product;
            PurchasedDate = purchasedDate;
            PurchasedAmount = purchasedAmount;
            SalesOrderHeaderID = salesOrderHeaderID;
        }

        /// <summary>
        /// Returns the total price amount of the order line.
        /// </summary>
        /// <returns>Returns a double</returns>
        public double FullPriceForLine()
        {
            double totalPrice = 0;
            for (int i = 0; i < PurchasedAmount; i++)
            {
                totalPrice += Product.SalesPrice;
            }
            return totalPrice;
        } 
    }
}
