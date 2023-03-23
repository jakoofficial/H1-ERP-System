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
        /// <param name="ordernumber"> int : Number for the order </param>
        /// <param name="timecreated"> DateTime : The time the order was created </param>
        /// <param name="implementationtime"> DateTime : The time the order was Implementated </param>
        /// <param name="customerId"> int : The id of the custommer </param>
        /// <param name="stage"> List<> : list of the different stages </param>
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
        /// <param name="Price"> int : Price for one item</param>
        /// <returns> The price for all items </returns>
        public int SaleOrderPrice(int Price)
        {
           foreach (SaleOrderLine Order in OrderLines)
            {
                
            }
            return Price;
        }
        
    }
}
