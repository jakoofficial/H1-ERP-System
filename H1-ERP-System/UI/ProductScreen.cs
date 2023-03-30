using H1_ERP_System.CompanyFolder;
using H1_ERP_System.ProductFolder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class ProductScreen : Screen
    {
        public override string Title { get; set; } = "List of tasks to do";
        public string ProductTitleSelected { get; set; } = "";
        /// <summary>
        /// Creates a table of Products using ListPage.
        /// </summary>
        protected override void Draw()
        {
            Clear(this);
            List<Product> productList = Database.GetProductslist();
            ListPage<Product> productListPage = new ListPage<Product>();
            for (int i = 0; i < productList.Count; i++)
                productListPage.Add(productList[i]);

            productListPage.AddColumn("Item Number", "ItemNumber");
            productListPage.AddColumn("Name", "Name");
            productListPage.AddColumn("In Stock", "QuantityInStock");
            productListPage.AddColumn("Sale Price", "SalesPrice");
            productListPage.AddColumn("Purchase Price", "PurchasePrice");
            productListPage.AddColumn("% Profit", "ProfitProcent");

            Product selected = productListPage.Select();
            if (selected != null)
            {
                Console.WriteLine("You selected: " + selected.Name);
                Clear(this);
                ProductDetails(selected); 
            }
            else
                ReturnToStart();
        }

        /// <summary>
        /// Method to show more information about selected product.
        /// </summary>
        /// <param name="selected"></param>
        public void ProductDetails(Product selected)
        {
            Clear(this);
            ListPage<Product> SelectedproductListPage = new ListPage<Product>();
             
            SelectedproductListPage.Add(selected);
            SelectedproductListPage.AddColumn("Item Number", $"ItemNumber");
            SelectedproductListPage.AddColumn("Name", "Name");
            SelectedproductListPage.AddColumn("Description", "Description");
            SelectedproductListPage.AddColumn("Sale Price", "SalesPrice");
            SelectedproductListPage.AddColumn("Purchase Price", "PurchasePrice");
            SelectedproductListPage.AddColumn("Location", "Location");
            SelectedproductListPage.AddColumn("In Stock", "QuantityInStock");
            SelectedproductListPage.AddColumn("Unit Type", "Unit");
            SelectedproductListPage.AddColumn("% Profit", "ProfitProcent");
            SelectedproductListPage.AddColumn("Profit in DKK.", "Profit");
            SelectedproductListPage.Draw();
            ReturnToStart();
        }

        public void ReturnToStart()
        {
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
            StartPage.StartUp();
        }
    }
}

