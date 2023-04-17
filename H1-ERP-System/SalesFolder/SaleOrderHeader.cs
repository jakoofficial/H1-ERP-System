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


    public class SaleOrderHeader
    {
        public int SaleOrderId { get; set; } // Ordernummer
        public string TimeCreated { get; set; } // Oprettelsetidspunkt
        public string ImplementationTime { get; set; } //Gennemførelsetidspunkt
        public Customer CustomerId { get; set; } //Kundenummer
        public int Customer_Id
        {
            get { return CustomerId.CustomerId; }
            set { }
        } //Kundenummer
        public string Customer_FullName
        {
            get { return CustomerId.FullName; }
            set { }
        } //Kunde Navn
        public double FullPrice { get; set; } //Kunde Navn
        public OrderStage Stage { get; set; } //Tilstand
        public List<SaleOrderLine> OrderLines { get; set; } //En liste af orderlinjer

        /// <summary>
        ///
        /// </summary>
        /// <param name="saleOrderId"></param>
        /// <param name="timecreated"></param>
        /// <param name="implementationtime"></param>
        /// <param name="customerId">Int (ID)</param>
        /// <param name="stage">Enum - Requires an Int. Maximum = 4</param>
        /// <param name="orderLines">List of SaleOrderLines</param>
        public SaleOrderHeader(int saleOrderId, string timecreated, string implementationtime, Customer customerId, OrderStage stage, List<SaleOrderLine> orderLines)
        {
            SaleOrderId = saleOrderId;
            TimeCreated = timecreated;
            ImplementationTime = implementationtime;
            CustomerId = customerId;
            Stage = stage;
            OrderLines = orderLines;
            FullPrice = SaleOrderPrice();
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
