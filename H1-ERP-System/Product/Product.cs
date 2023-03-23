﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.Product
{
    public class Product
    {
        public enum Units { Hours, Meters, Pieces, Kilogram }
        public int ItemNumber { get; set; } //Varenummer
        public string Name { get; set; } //Navn
        public string Description { get; set; } //Beskrivelse
        public double SalesPrice { get; set; } //Salgsspris
        public double PurchasePrice { get; set; } //Indkøbspris
        public string Location { get; private set; } //Lokation 
        public double QuantityInStock { get; set; } //Antal på lager
        public Units Unit { get; set; } //Enhed

        /// <summary>
        /// Location needs to be set with the method SetLocation()
        /// </summary>
        /// <param name="itemNumber"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="salesPrice"></param>
        /// <param name="purchasePrice"></param>
        /// <param name="quantityInStock"></param>
        /// <param name="unit"></param>
        public Product(int itemNumber, string name, string description, double salesPrice, double purchasePrice, double quantityInStock, Units unit)
        {
            this.ItemNumber = itemNumber;
            this.Name = name;
            this.Description = description;
            this.SalesPrice = salesPrice;
            this.PurchasePrice = purchasePrice;
            this.QuantityInStock = quantityInStock;
            this.Unit = unit;
        }

        /// <summary>
        /// Checks if the location is exactly 4 letters
        /// </summary>
        /// <param name="location"></param>
        /// <returns> Return true if there are 4 letters </returns>
        public bool SetLocation(string location)
        {
            if (location.Length == 4)
            {
                this.Location = location;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calculates the profit of the item
        /// </summary>
        /// <returns> Profit </returns>
        public double CalculateProfit()
        {
            return SalesPrice - PurchasePrice;
        }

        /// <summary>
        /// Calculates the profit margin of the item 
        /// </summary>
        /// <returns> String with percentage profit margin with 2 decimals </returns>
        public string CalculateProfitMargin()
        {
            double percentage = SalesPrice - PurchasePrice;
            percentage = (percentage / PurchasePrice) * 100;
            return $"{percentage.ToString("0.00")}% ";
        }
    }
}