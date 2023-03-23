using H1_ERP_System.CustomerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.CompanyFolder
{
    public enum Currency { DKK, USD, EUR}
    public class Company
    {
        public string CompanyName { get; set; } //Firmanavn
        public Address Address { get; set; } 
        public Currency Currency { get; set; } //Valuta
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="address"></param>
        /// <param name="currency"> Choose between DKK, USD or EUR. </param>
        public Company(string companyName, Address address, Currency currency)
        {
            CompanyName = companyName;
            Address = address;
            Currency = currency;
        }
    }
}
