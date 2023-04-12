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
        /// Used to get a list of companies from the database table using a queryString
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
                            Address adr = GetAddress($"SELECT * FROM dbo.Address WHERE AddressId = {(int)reader[2]}");
                            Company cp = new Company((int)reader[0], (string)reader[1], (Company.Currencies)(int)reader[3], adr.Street, adr.StreetNumber, adr.PostalCode, adr.City, adr.Country);
                            cList.Add(cp);
                        }
                    }
                }
                return cList;
            }
            return null;
        }

        /// <summary>
        /// Add company to the company table using a new Company
        /// </summary>
        /// <param name="cp">The Company that is being added</param>
        public static void AddCompany(Company cp)
        {
            AddAddress(new Address(0, cp.Street, cp.StreetNumber, cp.PostalCode, cp.City, cp.Country));
            Address a = GetAddress("SELECT * FROM dbo.Address ORDER BY AddressId DESC");

            string queryString = "INSERT INTO dbo.Companies " +
                "(CompanyName, AddressId, Currency) " +
                $"Values ('{cp.CompanyName}', {a.AddressId}, {(int)cp.Currency})";
            RunNonQuery(queryString);
        }

        /// <summary>
        /// Update the company using a specific company with it's changes
        /// </summary>
        /// <param name="cp"></param>
        public static void UpdateCompany(Company cp)
        {
            string queryString = "UPDATE dbo.Companies " +
                $"SET CompanyName='{cp.CompanyName}', AddressId={cp.AddressId}, Currency={(int)cp.Currency}" +
                $"WHERE CompanyId={cp.CompanyId}";
            RunNonQuery(queryString);
        }

        /// <summary>
        /// Remove the chosen company using the Company object
        /// </summary>
        /// <param name="cp"></param>
        public static void RemoveCompany(Company cp)
        {
            RemoveAddress(cp.AddressId);
            string queryString = $"DELETE FROM dbo.Companies WHERE CompanyId = {cp.CompanyId}";
            RunNonQuery(queryString);
        }
    }
}
