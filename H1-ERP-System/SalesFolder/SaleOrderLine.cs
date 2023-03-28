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
        public int Id { get; set; }
        public Product Product { get; set; }
        public string PurchasedDate { get; set; }
        public int PurchasedAmount { get; set; }
        public int SalesOrderHeaderID { get; set; }


        public SaleOrderLine(int id, Product product, string purchasedDate, int purchasedAmount, int salesOrderHeaderID)
        {     
            Id = id;
            Product = product;
            PurchasedDate = purchasedDate;
            PurchasedAmount = purchasedAmount;
            SalesOrderHeaderID = salesOrderHeaderID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns> The Price exlpained in summary </returns>
        public double FullPriceForLine()
        {
            double totalPrice = 0;
            for (int i = 0; i <= PurchasedAmount; i++)
            {
                Product.SalesPrice += totalPrice;
            }
            return totalPrice;
        } 
    }
}
