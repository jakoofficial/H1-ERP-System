using H1_ERP_System.CompanyFolder;
using H1_ERP_System.CustomerFolder;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System
{
    public partial class Database
    {
        /// <summary>
        /// Used to get a list of companies from the database table 
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static List<Company> GetCompanies(string queryString)
        {
            if (!queryString.IsNullOrEmpty())
            {
                List<Company> cList = new List<Company>();
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
                            cList.Add(cp);
                        }
                    }
                }
                return cList;
            }
            return null;
        }

        public static void AddCompany(Company cp)
        {
            AddAddress(cp.Address);
            Address a = GetAddress("SELECT * FROM dbo.Address ORDER BY AddressId DESC");

            string quesryString = "INSERT INTO dbo.Companies " +
                "(CompanyName, AddressId, Currency) " +
                $"Values ('{cp.CompanyName}', {a.Id}, {(int)cp.Currency})";
            RunNonQuery(quesryString);
        }

        public static void RemoveCompany(Company cp)
        {
            RemoveAddress(cp.Address.Id);
            string queryString = $"DELETE FROM dbo.Companies WHERE CompanyId = {cp.Id}";
            RunNonQuery(queryString);
        }
    }
}
