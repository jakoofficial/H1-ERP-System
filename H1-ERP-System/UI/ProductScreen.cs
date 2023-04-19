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
        public override string Title { get; set; } = "Product List";
        /// <summary>
        /// Creates a table of Products using ListPage.
        /// </summary>
        protected override void Draw()
        {
            Clear(this);

            List<Product> productList = Database.GetProductslist();
            ListPage<Product> productListPage = new ListPage<Product>();
            Console.WriteLine("Press F1 to Create a new product.");
            Console.WriteLine("Press F2 to Edit a product.");
            for (int i = 0; i < productList.Count; i++)
                productListPage.Add(productList[i]);

            productListPage.AddColumn("Item Number", "ItemNumber");
            productListPage.AddColumn("Name", "Name");
            productListPage.AddColumn("In Stock", "QuantityInStock");
            productListPage.AddColumn("Sale Price", "SalesPrice");
            productListPage.AddColumn("Purchase Price", "PurchasePrice");
            productListPage.AddColumn("% Profit", "ProfitProcent");
            productListPage.AddKey(ConsoleKey.F1, ProductEditScreen.CreateProduct);
            productListPage.AddKey(ConsoleKey.F2, ProductEditScreen.EditProduct);

            Product selected = productListPage.Select();
            if (selected != null)
                ProductDetails(selected);
            else
                Clear();
                Quit();
        }

        /// <summary>
        /// Method to show more information about selected product.
        /// </summary>
        /// <param name="selected"></param>
        public void ProductDetails(Product selected)
        {
            Title = $" {selected.Name} ";
            Clear(this);
            ListPage<Product> SelectedProductListPage = new ListPage<Product>();

            SelectedProductListPage.Add(selected);
            SelectedProductListPage.AddColumn("Item Number", "ItemNumber");
            SelectedProductListPage.AddColumn("Name", "Name");
            SelectedProductListPage.AddColumn("Description", "Description");
            SelectedProductListPage.AddColumn("Sale Price", "SalesPrice");
            SelectedProductListPage.AddColumn("Purchase Price", "PurchasePrice");
            SelectedProductListPage.AddColumn("Location", "Location");
            SelectedProductListPage.AddColumn("In Stock", "QuantityInStock");
            SelectedProductListPage.AddColumn("Unit Type", "Unit");
            SelectedProductListPage.AddColumn("% Profit", "ProfitProcent");
            SelectedProductListPage.AddColumn("Profit in DKK.", "Profit");

            SelectedProductListPage.Select();
            Console.ReadKey();
            Quit();
        }
    }
}

