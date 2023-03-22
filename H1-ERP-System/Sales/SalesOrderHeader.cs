using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.SalgsModul
{
    public enum Stage { None, Created, Confirmed, Packaged, Completed }


    public class SalesOrderHeader
    {
        public int OrderNumber { get; set; } // Ordernummer
        public string TimeCreated { get; set; } // Oprettelsetidspunkt
        public string ImplementationTime { get; set; } //Gennemførelsetidspunkt

        public int CustomerId { get; set; } //Kundenummer

        public Stage Stage { get; set; } //Tilstand

        List<SaleOrderLine> OrderLines = new List<SaleOrderLine>();  //En liste af orderlinjer
        

        public SalesOrderHeader(int ordernumber, string timecreated, string implementationtime, int customerId, Stage stage)
        {
            OrderNumber = ordernumber;
            TimeCreated = timecreated;
            ImplementationTime = implementationtime;
            CustomerId = customerId;
            Stage = stage;
        }

        public int SaleOrderPrice(int Price)
        {
           foreach (SaleOrderLine Order in OrderLines)
            {
                
            }
            return Price;
        }
        
    }
}
