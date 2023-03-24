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
        public static SalesOrderHeader SelectSaleOrderFromID(string queryString)
        {
            //string queryString = $"SELECT * FROM dbo.SalesOrders WHERE CustomerId = {id}";
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                Console.WriteLine(connection.State);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        SalesOrderHeader order = new SalesOrderHeader((int)reader[0],(DateTime)reader[1], (DateTime)reader[2], GetCustomerIDForOrder($"SELECT * FROM dbo.Customers WHERE CustomerId = {(int)reader[3]}"), (OrderStage)(int)reader[4]);
                        return order;
                    }
                }
                return null;
            }
        }

        public static Customer GetCustomerIDForOrder(string queryString)
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
                        Customer customer = new Customer((int)reader[0], (DateTime)reader[1], (string)reader[2], (string)reader[3], (Address)reader[4], (string)reader[5], (string)reader[6]);
                        return customer;
                    }
                }
                return null;
            }
        }

        //public static List<SalesOrderHeader> SelectAllOrders()
        //{
        //    List<SalesOrderHeader> orders = new List<SalesOrderHeader>();
        //    string queryString = $"SELECT * FROM dbo.SalesOrders";
        //    using (SqlConnection connection = Instance.GetConnection())
        //    {
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        connection.Open();
        //        Console.WriteLine(connection.State);
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Console.WriteLine(String.Format("{0}, {1}, {2}, {3}, {4}", reader[0], reader[1], reader[2], reader[3], reader[4]));
        //                orders.Add(new SalesOrderHeader((int)reader[0], (DateTime)reader[1], (DateTime)reader[2], (int)reader[3], (OrderStage)(int)reader[4]));
        //            }
        //        }
        //        return orders;
        //    }
        //}
    }
}
