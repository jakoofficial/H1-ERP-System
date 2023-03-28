using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H1_ERP_System.SalesFolder;
using H1_ERP_System.CustomerFolder;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

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
                        return order;
                    }
                }
                return null;
            }
        }

        public static Customer GetCustomer(string queryString)
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
                        Customer customer = new Customer((int)reader[0], (DateTime)reader[1], (string)reader[2], (string)reader[3], GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {(int)reader[4]}"), (string)reader[5], (string)reader[6]);
                        return customer;
                    }
                }
                return null;
            }
        }

        public static void AddSaleOrderToDB(SalesOrderHeader salesOrderHeader)
        {
            string queryString = $"INSERT INTO dbo.SalesOrders(OrderNumber = , TimeCreated, ImplementationTime, CustomerId, Stage) " +
                                 $"VALUES ({salesOrderHeader.OrderNumber},{salesOrderHeader.TimeCreated}, {salesOrderHeader.ImplementationTime}, {salesOrderHeader.CustomerId}, {salesOrderHeader.Stage})";

            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
            }
        }
    }
}
