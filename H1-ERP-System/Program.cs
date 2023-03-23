using TECHCOOL;
using H1_ERP_System;
using H1_ERP_System.Product;
using H1_ERP_System.SalgsModul;
using Microsoft.Data.SqlClient;

SqlConnection conn = Database.Instance.GetConnection();
Database.ReadOrderData();

Console.WriteLine(conn.State);