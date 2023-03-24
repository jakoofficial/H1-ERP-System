using H1_ERP_System.ProductFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.SalesFolder
{
    public enum Stage { None, Created, Confirmed, Packaged, Completed }


    public class SalesOrderHeader
    {
        public int OrderNumber { get; set; } // Ordernummer
        public DateTime TimeCreated { get; set; } // Oprettelsetidspunkt
        public DateTime ImplementationTime { get; set; } //Gennemførelsetidspunkt
        public int CustomerId { get; set; } //Kundenummer
        public Stage Stage { get; set; } //Tilstand
        List<SaleOrderLine> OrderLines = new List<SaleOrderLine>();  //En liste af orderlinjer
        
        /// <summary>
        /// Constructor for SalesOrderHeader
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <param name="timecreated"></param>
        /// <param name="implementationtime"></param>
        /// <param name="customerId"></param>
        /// <param name="stage"> Stage ; different stages for the order. </param>
        public SalesOrderHeader(int ordernumber, DateTime timecreated, DateTime implementationtime, int customerId, Stage stage)
        {
            OrderNumber = ordernumber;
            TimeCreated = timecreated;
            ImplementationTime = implementationtime;
            CustomerId = customerId;
            Stage = stage;
        }

       

        /// <summary>
        /// calculates all the prices in the list and returns the result
        /// </summary>
        /// <param name="Price"></param>
        /// <returns> The price for all items in total </returns>
        public double SaleOrderPrice()
        {
           double price = 0;
           foreach (SaleOrderLine line in OrderLines)
            {
                price += line.FullPriceForLine();
            }
            return price;
        }
        
    }
}
