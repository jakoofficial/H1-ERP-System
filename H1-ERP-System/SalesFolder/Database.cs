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
        public static SalesOrderHeader GetSalesOrders(string queryString)
        {
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                Console.WriteLine(connection.State);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        SalesOrderHeader order = new SalesOrderHeader((int)reader[0],(DateTime)reader[1], (DateTime)reader[2], GetCustomer($"SELECT * FROM dbo.Customers WHERE CustomerId = {(int)reader[3]}"), (OrderStage)(int)reader[4]);
                        order.OrderLines = CreateSaleOrderList((int)reader[0]);
                        return order;
                    }
                }
                return null;
            }
        }

        public static List<SaleOrderLine> CreateSaleOrderList(int SaleOrderHeaderID)
        {
            List<SaleOrderLine> order = new List<SaleOrderLine>();
            string queryString = $"SELECT * FROM dbo.SaleOrderLines WHERE OrderNumber={SaleOrderHeaderID}";
            using (SqlConnection connection = Instance.GetConnection())
            {
                
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                Console.WriteLine(connection.State);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order.Add(new SaleOrderLine((int)reader[0], GetProductFromID((int)reader[1]), (DateTime)reader[2], (int)reader[3], (int)reader[4]));
                        return order;
                    }
                }
                return null;
            }
        }

        public static void AddSaleOrderToDB(SalesOrderHeader s)
        {
            string queryString = $"INSERT INTO dbo.SalesOrders(OrderNumber, TimeCreated, ImplementationTime, CustomerId, Stage ) " +
                                 $"VALUES ({s.OrderNumber},{s.TimeCreated}, {s.ImplementationTime}, {s.CustomerId}, {s.Stage})";

            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
            }

            foreach (var item in s.OrderLines)
            {
                string queryString2 = "INSERT INTO dbo.SaleOrderLines(SaleOrderId, ProductId, PurchasedDate, PurhasedAmount, OrderNumber )" +
                                     $"VALUES ({item.Id}, {item.Product}, {item.PurchasedDate}, {item.PurchasedAmount}, {s.OrderNumber})";

                using (SqlConnection connection = Instance.GetConnection())
                {
                    SqlCommand command = new SqlCommand(queryString2, connection);
                    connection.Open();
                }
            }

        }

        
        public static void UpdateSaleOrder(SalesOrderHeader s)
        {
            string queryString = "UPDATE dbo.SalesOrders " +
                                $"SET OrderNumber={s.OrderNumber}, TimeCreated={s.TimeCreated}, ImplementationTime={s.ImplementationTime}, CustomerId={s.CustomerId}, Stage={s.Stage}";

            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
            }

            foreach (var item in s.OrderLines)
            {
                string queryString2 = "UPDATE dbo.SaleOrderLines " +
                                     $"SET SalesOrderID={item.Id}, ProductId{item.Product.ItemNumber}, PurchasedDate={item.PurchasedDate}, PurchasedAmount={item.PurchasedAmount}, OrderNumber={item.SalesOrderHeaderID}";

                using (SqlConnection connection = Instance.GetConnection())
                {
                    SqlCommand command = new SqlCommand(queryString2, connection);
                    connection.Open();
                }
            }

        }

    }
}
