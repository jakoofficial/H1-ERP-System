using H1_ERP_System.CustomerFolder;
using H1_ERP_System.ProductFolder;
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
        public static Customer GetCustomer(string queryString)
        {
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                //Console.WriteLine(connection.State);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Address adr = GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {(int)reader[4]}");
                        Customer customer = new Customer((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[5], (string)reader[6], adr.AddressId, adr.Street, adr.StreetNumber, adr.PostalCode, adr.City, adr.Country);
                        return customer;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Retrieves a list with all the Customer from our database
        /// </summary>
        /// <returns></returns>
        public static List<Customer> GetCustomerList()
        {
            List<Customer> cList = new List<Customer>();

            string queryString = $"SELECT * FROM dbo.Customers";
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Address adr = GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {(int)reader[4]}");
                        cList.Add(new Customer((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[5], (string)reader[6], adr.AddressId, adr.Street, adr.StreetNumber, adr.PostalCode, adr.City, adr.Country));
                        //cList.Add(new Customer((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {(int)reader[4]}"), (string)reader[5], (string)reader[6]));                       
                    }
                }
            }
            return cList;
        }
        /// <summary>
        /// Inserts a product into our database
        /// </summary>
        /// <param name="c"> The Customer that will be added to our database </param>
        public static void AddCustomerToDB(Customer c)
        {
            AddAddress(new Address(0, c.Street, c.StreetNumber, c.PostalCode, c.City, c.Country));
            Address a = GetAddress("SELECT * FROM dbo.Address ORDER BY AddressId DESC");

            string queryString = "INSERT INTO dbo.Customers " +
                "(LastPurchased, FirstName, LastName, AddressId, PhoneNumber, Email) " +
                "VALUES " +
                $"('{c.LastPurchase}', '{c.FirstName}', '{c.LastName}', {a.AddressId}, '{c.PhoneNumber}', '{c.Email}');";
            RunNonQuery(queryString);
        }

        /// <summary>
        /// Updates a Customer in the database
        /// </summary>
        /// <param name="c"> The updated Customer </param>
        public static void UpdateCustomer(Customer c)
        {
            UpdateAddress(new Address(0, c.Street, c.StreetNumber, c.PostalCode, c.City, c.Country));
            string queryString = "UPDATE dbo.Customers " +
                $"SET LastPurchased='{c.LastPurchase}', FirstName='{c.FirstName}', LastName='{c.LastName}', PhoneNumber={c.PhoneNumber}, Email='{c.Email}' " +
                $"WHERE CustomerId={c.CustomerId}";
            RunNonQuery(queryString);
        }

        /// <summary>
        /// deletes a Customer from the database and the address the customer has in the database
        /// </summary>
        /// <param name="c"></param>
        public static void DeleteCustomer(Customer c)
        {
            RemoveAddress(c.AddressId);
            string queryString = $"DELETE FROM dbo.Customers WHERE CustomerId={c.CustomerId}";
            RunNonQuery(queryString);
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
        /// Add an address when adding a new Company or Customer
        /// </summary>
        /// <param name="adr">The Address of the Company or Customer</param>
        public static void AddAddress(Address adr)
        {
            string quesryString = "INSERT INTO dbo.Address " +
                "(Street, StreetNumber, PostalCode, City, Country) " +
                $"Values ('{adr.Street}', '{adr.StreetNumber}', '{adr.PostalCode}', '{adr.City}', '{adr.Country}')";
            RunNonQuery(quesryString);
        }

        /// <summary>
        /// Update the address from a company or customer using their Address
        /// </summary>
        /// <param name="adr">Address from the Company or Customer</param>
        public static void UpdateAddress(Address adr)
        {
            string queryString = "UPDATE dbo.Address " +
                $"SET Street='{adr.Street}', StreetNumber='{adr.StreetNumber}', PostalCode='{adr.PostalCode}', City='{adr.City}', Country='{adr.Country}'" +
                $"WHERE AddressId={adr.AddressId}";
            RunNonQuery(queryString);
        }

        /// <summary>
        /// Remove the Address from the table using Company or Customer Address Id
        /// </summary>
        /// <param name="id">Address ID from the address on Company or Customer</param>
        public static void RemoveAddress(int id)
        {
            string queryString = $"DELETE FROM dbo.Address WHERE AddressId = {id}";
            RunNonQuery(queryString);
        }
    }
}
