using TECHCOOL;
using H1_ERP_System;
using Microsoft.Data.SqlClient;
using H1_ERP_System.ProductFolder;
using H1_ERP_System.CompanyFolder;
using H1_ERP_System.CustomerFolder;
using H1_ERP_System.SalesFolder;

SqlConnection conn = Database.Instance.GetConnection();
Database.ReadOrderData();
//Database.SelectAllOrders();
//Database.SelectSaleOrderFromID(1);

Database.GetSalesOrders("SELECT * FROM dbo.SalesOrders WHERE CustomerId = 1");
Console.ReadLine();
