using H1_ERP_System.CustomerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL;
using Microsoft.Data.SqlClient;

namespace H1_ERP_System
{
    public partial class Database
    {
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
    }
}
