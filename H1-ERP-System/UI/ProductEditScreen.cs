﻿using H1_ERP_System.ProductFolder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class ProductEditScreen : Screen
    {
        public override string Title { get; set; } = "Edit Product";
        public Product? Pr { get; set; }
        public ProductEditScreen(Product pr = null)
        {
            Pr = pr;
        }
        protected override void Draw()
        {
            Clear(this);
            
        }

        public static void CreateProduct(Product pr)
        {
            Console.Clear();
            Form<Product> pEditor = new Form<Product>();
            pr = new Product();

            pEditor.TextBox("Product ID", "ItemNumber");
            pEditor.TextBox("Product Name", "Name");
            pEditor.TextBox("Description", "Description");
            pEditor.TextBox("Sale Price", "SalesPrice");
            pEditor.TextBox("Purchase Price", "PurchasePrice");
            pEditor.TextBox("Location", "Location");
            pEditor.TextBox("In Stock", "QuantityInStock");
            pEditor.SelectBox("Unit Type", "Unit");
            pEditor.AddOption("Unit Type", "Hours", Product.Units.Hours);
            pEditor.AddOption("Unit Type", "Meters", Product.Units.Meters);
            pEditor.AddOption("Unit Type", "Pieces", Product.Units.Pieces);
            pEditor.AddOption("Unit Type", "Kilogram", Product.Units.Kilogram);

        CreateProduct:
            Console.WriteLine("Press ESC When Done\n");
            pEditor.Edit(pr);

            pr.Profit = pr.CalculateProfit();
            pr.ProfitProcent = pr.CalculateProfitMargin();

            if (pr.ProfitProcent == "0")
            {
                //Checks if the new Product object is empty, or not. 
                //If not empty: Update the product with the new information, and send the user back to the ProductScreen
                //If empty: if object is empty/has an empty value, give user option to go again (goto createProduct) or exit to ProductScreen. 

                if (!Checker.ChecksIfEmpty(pr))
                {
                    Database.AddProductToDB(pr);
                    Console.Clear();
                    Console.WriteLine("S Successfully Created");
                    Console.ReadLine();
                    Screen.Display(new ProductScreen());
                }
                else
                {
                    Retry(new ProductScreen());
                    goto CreateProduct;
                }
            }
        }



        public static void EditProduct(Product p)
        {
            Clear();

        editProduct:

            int originalItemNumber = p.ItemNumber;
            Form<Product> pEditor = new Form<Product>();

            pEditor.TextBox("Item number", "ItemNumber");
            pEditor.TextBox("Product Name", "Name");
            pEditor.TextBox("Description", "Description");
            pEditor.TextBox("Sale Price", "SalesPrice");
            pEditor.TextBox("Purchase Price", "PurchasePrice");
            pEditor.TextBox("Location (4 char)", "Location");
            pEditor.TextBox("In Stock", "QuantityInStock");
            pEditor.SelectBox("Unit Type", "Unit");
            pEditor.AddOption("Unit Type", "Hours", Product.Units.Hours);
            pEditor.AddOption("Unit Type", "Meters", Product.Units.Meters);
            pEditor.AddOption("Unit Type", "Pieces", Product.Units.Pieces);
            pEditor.AddOption("Unit Type", "Kilogram", Product.Units.Kilogram);

            Console.WriteLine("Press ESC When Done\n");
            pEditor.Edit(p);

            //Checks if the Product object is empty, or not. 
            //If not empty: Update the product with the new information, and send the user back to the ProductScreen
            //If empty: if object is empty/has an empty value, give user option to go again (goto editProduct) or exit to ProductScreen. 
            if (!Checker.ChecksIfEmpty(p) && p.SetLocation(p.Location))
            {
                Database.UpdateProduct(p, originalItemNumber);
                Console.Clear();
                Console.WriteLine("SSuccessfully Updated");
                Console.ReadLine();
                Screen.Display(new ProductScreen());
            }
            else
            {
                Retry(new ProductScreen());
                goto editProduct;
            }
        }
        public static void Retry(object screen)
        {
            Console.Clear();
            Console.WriteLine("TThere might be an empty value, please make sure everything has a value.\n\n" +
                              "Press ENTER to try again\n" +
                              "Or ESCAPE to quit editing\n");
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.Escape)
            {
                Screen.Display((Screen)screen);
            }
            else if (key == ConsoleKey.Enter)
            {
                return;
            }
        }
    }
}