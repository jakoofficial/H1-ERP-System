using H1_ERP_System.ProductFolder;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System
{
    public partial class Database
    {
        /// <summary>
        /// Used every time we need to run a non query
        /// </summary>
        /// <param name="queryString"> The query that will be executed </param>
        public static void RunNonQuery(string queryString)
        {
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves the Product with the specified ItemNumber/ItemId From our database
        /// </summary>
        /// <param name="itemID"> ItemNumber/ItemId of the item that will be retrieved </param>
        /// <returns></returns>
        public static Product GetProductFromID(int itemID)
        {
            string queryString = $"SELECT * FROM dbo.Products WHERE ItemID = {itemID}";
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Product p = new Product((int)reader[0], (string)reader[1], (string)reader[2], (double)reader[3], (double)reader[4], (double)reader[6], (Product.Units)(int)reader[7]);
                    p.SetLocation((string)reader[5]);
                    Console.WriteLine($"Test {p.Unit}");
                    Console.WriteLine(String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7]));
                    return p;
                }
            }
        }

        /// <summary>
        /// Retrieves a list with all the Products from our database
        /// </summary>
        /// <returns></returns>
        public static List<Product> GetProductslist()
        {
            List<Product> pList = new List<Product>();
            int i = 0;

            string queryString = $"SELECT * FROM dbo.Products";
            using (SqlConnection connection = Instance.GetConnection())
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pList.Add(new Product((int)reader[0], (string)reader[1], (string)reader[2], (double)reader[3], (double)reader[4], (double)reader[6], (Product.Units)(int)reader[7]));
                        pList[i++].SetLocation((string)reader[5]);
                    }
                }
            }
            return pList;
        }

        /// <summary>
        /// Inserts a product into our database
        /// </summary>
        /// <param name="p"> The product that will be added to our database </param>
        public static void AddProductToDB(Product p)
        {
            string queryString = "INSERT INTO dbo.Products " +
                "(ItemId, Name, Description, SalesPrice, PurchasePrice, Location, QuantityInStock, Unit) " +
                "VALUES " +
                $"({p.ItemNumber}, '{p.Name}', '{p.Description}', {p.SalesPrice}, {p.PurchasePrice}, '{p.Location}', {p.QuantityInStock}, {(int)p.Unit});";
            RunNonQuery(queryString);
        }

        /// <summary>
        /// Updates an item in the database
        /// </summary>
        /// <param name="p"> The updated item </param>
        /// <param name="originalItemNumber"> The originalItemNumber, this is needed in case the ItemNumber is changed </param>
        public static void UpdateProduct(Product p, int originalItemNumber)
        {
            string queryString = "UPDATE dbo.Products " +
                $"SET ItemId={p.ItemNumber}, Name='{p.Name}', Description='{p.Description}', SalesPrice={p.SalesPrice}, PurchasePrice={p.PurchasePrice}, Location='{p.Location}', QuantityInStock={p.QuantityInStock}, Unit={(int)p.Unit} " +
                $"WHERE ItemId={originalItemNumber}";
            RunNonQuery(queryString);
        }

        /// <summary>
        /// Delete a specified item from our database
        /// </summary>
        /// <param name="itemNumber"> ItemNumber/ItemId of the item that will be delete from our database </param>
        public static void DeleteProduct(int itemNumber)
        {
            string queryString = $"Delete FROM dbo.Products WHERE ItemId={itemNumber}";
            RunNonQuery(queryString);
        }
    }
}
