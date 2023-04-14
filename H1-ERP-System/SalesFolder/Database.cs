using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP_System.SalesFolder;
using H1_ERP_System.CustomerFolder;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using H1_ERP_System.ProductFolder;

namespace H1_ERP_System
{
    public partial class Database
    {
        /// <summary>
        /// Gets SalesOrder data from database by using QueryString.
        /// And returns a new SaleOrderHeader.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static List<SalesOrderHeader> GetSalesOrders(string queryString)
        {
            List<SalesOrderHeader> orderList = new List<SalesOrderHeader>();
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                //Console.WriteLine(connection.State);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalesOrderHeader order = new SalesOrderHeader((int)reader[0], (string)reader[1], (string)reader[2], GetCustomer($"SELECT * FROM dbo.Customers WHERE CustomerId = {(int)reader[3]}"), (OrderStage)(int)reader[4], CreateSaleOrderList((int)reader[0]));
                        //order.OrderLines = CreateSaleOrderList((int)reader[0]);
                        orderList.Add(order);
                    }
                    return orderList;
                }
                return null;
            }
        }

        /// <summary>
        /// Creates a list for SaleOrderheader - "OrderLines", and returns it.
        /// </summary>
        /// <param name="SaleOrderHeaderID"> The SaleOrderHeader Íd where the SaleOrderLines are under. </param>
        /// <returns></returns>
        public static List<SaleOrderLine> CreateSaleOrderList(int SaleOrderHeaderID)
        {
            List<SaleOrderLine> order = new List<SaleOrderLine>();
            string queryString = $"SELECT * FROM dbo.SalesOrderLines WHERE OrderNumber={SaleOrderHeaderID}";
            using (SqlConnection connection = Instance.GetConnection())
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                //Console.WriteLine(connection.State);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order.Add(new SaleOrderLine((int)reader[0], GetProductFromID((int)reader[1]), (string)reader[2], (int)reader[3], (int)reader[4]));
                    }
                }
                return order;
                //return null;
            }
        }

        /// <summary>
        /// Adds a saleOrderHeader with a OrderLine.
        /// </summary>
        /// <param name="s"></param>
        public static void AddSaleOrderToDB(SalesOrderHeader s)
        {
            string queryString = $"INSERT INTO dbo.SalesOrders( TimeCreated, ImplementationTime, CustomerId, Stage ) " +
                                 $"VALUES ('{s.TimeCreated}', '{s.ImplementationTime}', {(int)s.CustomerId.CustomerId}, {(int)s.Stage})";

            RunNonQuery(queryString);

            foreach (var item in s.OrderLines)
            {
                string queryString2 = "INSERT INTO dbo.SalesOrderLines(ProductId, PurchasedDate, PurchasedAmount, OrderNumber )" +
                                     $"VALUES ({item.Product.ItemNumber}, '{item.PurchasedDate}', {item.PurchasedAmount}, {s.OrderNumber})";

                RunNonQuery(queryString2);
            }

        }


        /// <summary>
        /// Updates a SaleOrderHeader by writing the new one in as "s"
        /// also updates OrderLines(the list of SaleOrderLines)
        /// </summary>
        /// <param name="s"></param>
        public static void UpdateSaleOrder(SalesOrderHeader s)
        {
            string queryString = "UPDATE dbo.SalesOrders " +
                                $"SET ImplementationTime='{s.ImplementationTime}', CustomerId={s.CustomerId.CustomerId}, Stage={(int)s.Stage} " +
                                $"WHERE OrderNumber={s.OrderNumber}";

            RunNonQuery(queryString);

            foreach (var item in s.OrderLines)
            {
                string queryString2 = "UPDATE dbo.SalesOrderLines " +
                                     $"SET ProductId={item.Product.ItemNumber}, PurchasedDate='{item.PurchasedDate}', PurchasedAmount={item.PurchasedAmount}, OrderNumber={item.SalesOrderHeaderID} " +
                                     $"WHERE SalesOrderId={item.SaleOrderLineId}";

                RunNonQuery(queryString2);
            }
        }

        /// <summary>
        /// Removes the saleorder by using it's ID.
        /// </summary>
        /// <param name="id"> id = OrderNumber</param>
        public static void RemoveSaleOrder(int id)
        {
            string queryString = $"DELETE dbo.SalesOrders WHERE OrderNumber={id}";

            RunNonQuery(queryString);
        }


    }
}
