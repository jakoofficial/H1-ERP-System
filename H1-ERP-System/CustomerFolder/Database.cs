using H1_ERP_System.CustomerFolder;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System
{
    public partial class Database
    {
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
        /// 
        /// </summary>
        /// <param name="adr"></param>
        public static void AddAddress(Address adr)
        {
            string quesryString = "INSERT INTO dbo.Address " +
                "(Street, StreetNumber, PostalCode, City, Country) " +
                $"Values ('{adr.Street}', '{adr.StreetNumber}', '{adr.PostalCode}', '{adr.City}', '{adr.Country}')";
            RunNonQuery( quesryString );
        }

        public static void UpdateAddress(Address adr)
        {
            string queryString = "UPDATE dbo.Address " +
                $"SET Street='{adr.Street}', StreetNumber='{adr.StreetNumber}', PostalCode='{adr.PostalCode}', City='{adr.City}', Country='{adr.Country}'" +
                $"WHERE AddressId={adr.Id}";
            RunNonQuery(queryString);
        }

        public static void RemoveAddress(int id)
        {
            string queryString = $"DELETE FROM dbo.Address WHERE AddressId = {id}";
            RunNonQuery(queryString);
        }
    }
}
