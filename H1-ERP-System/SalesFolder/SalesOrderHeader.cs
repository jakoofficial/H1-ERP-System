using H1_ERP_System.CustomerFolder;
using H1_ERP_System.ProductFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.SalesFolder
{
    public enum OrderStage { None, Created, Confirmed, Packaged, Completed }


    public class SalesOrderHeader
    {
        public int OrderNumber { get; set; } // Ordernummer
        public string TimeCreated { get; set; } // Oprettelsetidspunkt
        public string ImplementationTime { get; set; } //Gennemførelsetidspunkt
        public Customer CustomerId { get; set; } //Kundenummer
        public OrderStage Stage { get; set; } //Tilstand
        public List<SaleOrderLine> OrderLines { get; set; } = new List<SaleOrderLine>(); //En liste af orderlinjer
        
        /// <summary>
        /// Constructor for SalesOrderHeader
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <param name="timecreated"></param>
        /// <param name="implementationtime"></param>
        /// <param name="customerId"></param>
        /// <param name="stage"> Stage ; different stages for the order. </param>
        public SalesOrderHeader(int ordernumber, string timecreated, string implementationtime, Customer customerId, OrderStage stage)
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
