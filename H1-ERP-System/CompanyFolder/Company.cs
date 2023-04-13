using H1_ERP_System.CustomerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.CompanyFolder
{
    public class Company : Address
    {
        public int CompanyId { get; set; }
        public enum Currencies { DKK, USD, EUR }
        public string CompanyName { get; set; } //Firmanavn
        public Currencies Currency { get; set; } //Valuta

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Company() : base(0,"", "", "", "", "")
        {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="address"></param>
        /// <param name="currency"> Choose between DKK, USD or EUR. </param>
        public Company(int id, string companyName, Currencies currency, int addressId, string street, string streetNumber, string postalCode, string city, string country) 
            : base(addressId, street, streetNumber, postalCode, city, country)
        {
            CompanyId = id;
            CompanyName = companyName;
            Currency = currency;
        }
    }
}
