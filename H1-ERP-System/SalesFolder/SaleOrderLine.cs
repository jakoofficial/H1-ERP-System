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
        private Product Product { get; set; }
        private DateTime PurchasedDate { get; set; }
        private int PurchasedAmount { get; set; }


        public SaleOrderLine(Product product, DateTime purchasedDate, int purchasedAmount)
        {     
            Product = product;
            PurchasedDate = purchasedDate;
            PurchasedAmount = purchasedAmount;
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
