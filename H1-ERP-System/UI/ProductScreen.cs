using H1_ERP_System.CompanyFolder;
using H1_ERP_System.CustomerFolder;
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
            //Clear(this);

            List<Product> productList = Database.
                GetProductslist();
            ListPage<Product> productListPage = new ListPage<Product>();
            if (productList.Count != 0)
            {
                for (int i = 0; i < productList.Count; i++)
                    productListPage.Add(productList[i]);

                productListPage.AddColumn("Item Number", "ItemNumber");
                productListPage.AddColumn("Name", "Name");
                productListPage.AddColumn("In Stock", "QuantityInStock");
                productListPage.AddColumn("Sale Price", "SalesPrice");
                productListPage.AddColumn("Purchase Price", "PurchasePrice");
                productListPage.AddColumn("% Profit", "ProfitProcent");
                productListPage.AddKey(ConsoleKey.F1, ProductEditScreen.CreateProduct);
                productListPage.AddKey(ConsoleKey.F5, ProductEditScreen.DeleteProductScreen);

                Console.WriteLine("Enter  | Select\n" +
                                  "F1     | Create new\n" +
                                  "F5     | Delete\n" +
                                  "ESC    | Go back");

                Product selected = productListPage.Select();
                if (selected != null)
                    ProductDetails(selected);
                else
                    Clear();
                Quit();
            }
            else
            {
                ProductEditScreen.CreateProduct(new Product());
            }
        }

        public void ProductDetails(Product selected)
        {
            Title = $" {selected.Name} ";
            Clear(this);

            ListPage<Product> selectedProductListPage = new ListPage<Product>();
            selectedProductListPage.Add(selected);

            selectedProductListPage.AddColumn("Item Number", "ItemNumber");
            selectedProductListPage.AddColumn("Name", "Name");
            selectedProductListPage.AddColumn("Description", "Description");
            selectedProductListPage.AddColumn("Sale Price", "SalesPrice");
            selectedProductListPage.AddColumn("Purchase Price", "PurchasePrice");
            selectedProductListPage.AddColumn("Location", "Location");
            selectedProductListPage.AddColumn("In Stock", "QuantityInStock");
            selectedProductListPage.AddColumn("Unit Type", "Unit");
            selectedProductListPage.AddColumn("% Profit", "ProfitProcent");
            selectedProductListPage.AddColumn("Profit in DKK.", "Profit");
            selectedProductListPage.AddKey(ConsoleKey.F2, ProductEditScreen.EditProduct);

            Console.WriteLine("F2  | Edit\n" +
                              "ESC | Go back");

            selectedProductListPage.Select();

            Console.Clear();
            Title = "Product List";
        }
    }
}

