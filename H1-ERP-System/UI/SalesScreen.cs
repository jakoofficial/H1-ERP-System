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
            ListPage<SaleOrderHeader> salesList = new ListPage<SaleOrderHeader>();
            List<SaleOrderHeader> orders = Database.GetSaleOrders($"SELECT * FROM SaleOrders");

            if (orders.Count != 0)
            {
                for (int i = 0; i < orders.Count; i++)
                {
                    salesList.Add(orders[i]);
                }

                salesList.AddColumn("Sale ID", "SaleOrderId");
                salesList.AddColumn("Purchase date", "TimeCreated", 30);
                salesList.AddColumn("Customer ID", "Customer_Id");
                salesList.AddColumn("Customer Name", "Customer_FullName", 30);
                salesList.AddColumn("Total", "FullPrice");
                salesList.AddKey(ConsoleKey.F1, SalesEditScreen.CreateSale);
                salesList.AddKey(ConsoleKey.F5, SalesEditScreen.DeleteSale);

                Console.WriteLine("F1     | Create new\n" +
                                  "F5     | Delete\n" +
                                  "ESC    | Go back");
                SaleOrderHeader s = salesList.Select();
                if (s != null)
                {
                    Clear(this);
                    ShowOrderLines(s);
                }
                Clear(this);
                Quit();
            }
            else
            {
                SalesEditScreen.CreateSale(null);
            }
        }

        //Displays the Order Lines.
        public void ShowOrderLines(SaleOrderHeader salesOrder)
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
            lpSal.AddKey(ConsoleKey.F2, SalesEditScreen.EditSale);
            //lpSal.AddKey(ConsoleKey.F2, SalesOrderEdit.CreateSale);

            Console.WriteLine("Press F2 to edit Selected\n" +
                              //"Press F2 to create new | WIP\n" +
                              "Press ESC to go back");

            SaleOrderLine sl = lpSal.Select();

            Quit();
        }
    }
}