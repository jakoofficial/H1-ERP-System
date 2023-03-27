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

namespace H1_ERP_System
{
    public partial class Database
    {
        //Company c = Database.GetCompany("SELECT * FROM dbo.Companies WHERE CompanyId = 1");
        //Address a = c.Address;

        //Console.WriteLine(a.Country);
       
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
            sb.DataSource = "docker.data.techcollege.dk";
            sb.InitialCatalog = "H1PD021123_Gruppe2";
            sb.TrustServerCertificate = true;
            sb.UserID = "H1PD021123_Gruppe2";
            sb.Password = "H1PD021123_Gruppe2";
            string connectionString = sb.ToString();
            Console.WriteLine(connectionString);

            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        public static Company GetCompany(string queryString)
        {
            if (!queryString.IsNullOrEmpty())
            {
                using (SqlConnection connection = Instance.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(queryString, connection);
                    connection.Open();
                    //Debug.WriteLine("Connectoin {0}", connection.State);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Company cp = new Company((int)reader[0], (string)reader[1], GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {(int)reader[2]}"), (Company.Currencies)(int)reader[3]);
                            return cp;
                        }
                    }
                }
            }
            return null;
        }

        public static Address GetAddress(string queryString)
        {
            if (!queryString.IsNullOrEmpty())
            {
                using (SqlConnection connection = Instance.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(queryString, connection);
                    connection.Open();
                    //Debug.WriteLine("Connectoin {0}", connection.State);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Address adr = new Address((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5]);
                            return adr;
                        }
                    }
                }
            }
            return null;
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
