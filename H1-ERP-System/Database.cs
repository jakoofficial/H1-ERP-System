using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using H1_ERP_System.CompanyFolder;
using H1_ERP_System.CustomerFolder;
using H1_ERP_System.UI;

namespace H1_ERP_System
{
    public partial class Database
    {

        //UserID & Password: H1PD021123_Gruppe2
        public static string username = "";
        public static string password = "";
       

        public static Database Instance { get; }
        static Database()
        {
            Instance = new Database();
        }

        /// <summary>
        ///  Creates a string with all the necessary data needed to login on the SQL Server.
        /// </summary>
        /// <returns> SQL connection we created </returns>
        public SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder sb = new();
            sb.DataSource = "192.168.1.70";
            sb.InitialCatalog = "H1PD021123_Gruppe2";
            sb.TrustServerCertificate = true;
            sb.UserID = username;
            sb.Password = password;
            string connectionString = sb.ToString();

            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        /// <summary>
        /// Creates a queryString to select the data that is needed.
        /// uses "connection.Open" under "using ()" to open the connection.
        /// And closes it after automatically.
        /// </summary>
        public static void ReadOrderData()
        {
            string queryString = "SELECT OrderID, CustomerID FROM dbo.Orders;";
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                Console.WriteLine(connection.State);
                //using (SqlDataReader reader = command.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
                //    }
                //}
            }
        }
    }
}
