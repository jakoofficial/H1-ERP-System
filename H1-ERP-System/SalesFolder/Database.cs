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
        /// Gets SalesOrder data from database by using a query string, and returns a new SaleOrderHeader.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static List<SaleOrderHeader> GetSaleOrders(string queryString)
        {
            List<SaleOrderHeader> orderList = new List<SaleOrderHeader>();
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SaleOrderHeader order = new SaleOrderHeader((int)reader[0], (string)reader[1], (string)reader[2], GetCustomer($"SELECT * FROM dbo.Customers WHERE CustomerId = {(int)reader[3]}"), (OrderStage)(int)reader[4], CreateSaleOrderList((int)reader[0]));
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
            string queryString = $"SELECT * FROM dbo.SaleOrderLine WHERE SaleOrderId={SaleOrderHeaderID}";
            using (SqlConnection connection = Instance.GetConnection())
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order.Add(new SaleOrderLine((int)reader[0], GetProductFromID((int)reader[1]), (string)reader[2], (int)reader[3], (int)reader[4]));
                    }
                }
                return order;
            }
        }

        /// <summary>
        /// Adds a SaleOrderHeader with a OrderLine.
        /// </summary>
        /// <param name="s"></param>
        public static void AddSaleOrderToDB(SaleOrderHeader s)
        {
            string queryString = $"INSERT INTO dbo.SaleOrders( TimeCreated, ImplementationTime, CustomerId, Stage ) " +
                                 $"VALUES ('{s.TimeCreated}', '{s.ImplementationTime}', {(int)s.CustomerId.CustomerId}, {(int)s.Stage})";

            RunNonQuery(queryString);

            foreach (var item in s.OrderLines)
            {
                string queryString2 = "INSERT INTO dbo.SaleOrderLine(ProductId, PurchasedDate, PurchasedAmount, SaleOrderLineId )" +
                                     $"VALUES ({item.Product.ItemNumber}, '{item.PurchasedDate}', {item.PurchasedAmount}, {s.SaleOrderId})";

                RunNonQuery(queryString2);
            }

        }


        /// <summary>
        /// Updates the selected SaleOrderHeader by using it's ID. Also updates OrderLines(the list of SaleOrderLines)
        /// </summary>
        /// <param name="s"></param>
        public static void UpdateSaleOrder(SaleOrderHeader s)
        {
            string queryString = "UPDATE dbo.SaleOrders " +
                                $"SET ImplementationTime='{s.ImplementationTime}', CustomerId={s.CustomerId.CustomerId}, Stage={(int)s.Stage} " +
                                $"WHERE SaleOrderLineId={s.SaleOrderId}";

            RunNonQuery(queryString);

            foreach (var item in s.OrderLines)
            {
                string queryString2 = "UPDATE dbo.SaleOrderLine " +
                                     $"SET ProductId={item.Product.ItemNumber}, PurchasedDate='{item.PurchasedDate}', PurchasedAmount={item.PurchasedAmount}, SaleOrderLineId={item.SalesOrderHeaderID} " +
                                     $"WHERE SalesOrderId={item.SaleOrderLineId}";

                RunNonQuery(queryString2);
            }
        }

        /// <summary>
        /// Removes the SaleOrderHeader by using it's ID.
        /// </summary>
        /// <param name="id"> id = SaleOrderHeaderId</param>
        public static void RemoveSaleOrderHeader(int id)
        {
            string queryString = $"DELETE dbo.SaleOrders WHERE SaleOrderId={id}";

            RunNonQuery(queryString);
        }
    }
}
