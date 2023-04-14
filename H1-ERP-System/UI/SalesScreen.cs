using H1_ERP_System.CompanyFolder;
using H1_ERP_System.CustomerFolder;
using H1_ERP_System.SalesFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class SalesScreen : Screen
    {
        /// <summary>
        /// SalesScreen displays all of the Sales (headers), in the company.
        /// Displays: Sale ID, Purchase date, Customer ID, Customer Name and total amount paid for each Header.
        /// 
        /// User can select a header, to see it's order lines.
        /// Displays: Order ID, Purchase date and Product name.
        /// </summary>
        public override string Title { get; set; } = "Sales";
        protected override void Draw()
        {
            ShowOrderHeader();
        }

        //Displays the Order Header.
        public void ShowOrderHeader()
        {
            ListPage<SalesOrderHeader> salesList = new ListPage<SalesOrderHeader>();
            List<SalesOrderHeader> orders = Database.GetSalesOrders($"SELECT * FROM SalesOrders");

            for (int i = 0; i < orders.Count; i++)
            {
                salesList.Add(orders[i]);
            }

            salesList.AddColumn("Sale ID", "OrderNumber");
            salesList.AddColumn("Purchase date", "TimeCreated", 30);
            salesList.AddColumn("Customer ID", "Customer_Id");
            salesList.AddColumn("Customer Name", "Customer_FullName", 30);
            salesList.AddColumn("Total", "FullPrice");

            SalesOrderHeader s = salesList.Select();
            Clear(this);
            ShowOrderLines(s);
        }

        //Displays the Order Lines.
        public void ShowOrderLines(SalesOrderHeader salesOrder)
        {
            ListPage<SaleOrderLine> lpSal = new ListPage<SaleOrderLine>();
            for (int i = 0; i < salesOrder.OrderLines.Count; i++)
            {
                lpSal.Add(salesOrder.OrderLines[i]);
            }
            Console.WriteLine($"\nCustomer ID: {salesOrder.Customer_Id}");
            Console.WriteLine($"Customer: {salesOrder.Customer_FullName}");

            lpSal.AddColumn("Order ID", "SalesOrderHeaderID", 10);
            lpSal.AddColumn("Purchase date ", "PurchasedDate");
            lpSal.AddColumn("Product name", "ProductName", 25);
            lpSal.Draw();
            ReturnToStart();
        }
        public void ReturnToStart()
        {
            Console.WriteLine("Press any key to go back to the main page.");
            Console.ReadKey();
            StartPage.StartUp();
        }
    }
}