using H1_ERP_System.CustomerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.CompanyFolder
{
    public class Company
    {
        public int Id { get; set; }
        public enum Currencies { DKK, USD, EUR }
        public string CompanyName { get; set; } //Firmanavn
        public Address Address { get; set; }
        public Currencies Currency { get; set; } //Valuta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="address"></param>
        /// <param name="currency"> Choose between DKK, USD or EUR. </param>
        public Company(int id, string companyName, Address address, Currencies currency)
        {
            Id = id;
            CompanyName = companyName;
            Address = address;
            Currency = currency;
        }
    }
}
