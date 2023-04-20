using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1_ERP_System.UI
{
    public class Login
    {

        public static void LoginInput()
        {
            while (true)
            {
                Console.WriteLine("===Login===\n");
                Console.Write("Username: ");
                string inputUser = Console.ReadLine();
                Console.Write("Password: ");
                string inputPass = Console.ReadLine();
                Database.username = inputUser;
                Database.password = inputPass;

                try
                {
                    Database.Instance.GetConnection().Open();
                    break;
                }
                catch (SqlException SqlExcep)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nIncorrect Username or Password");
                    Console.ForegroundColor= ConsoleColor.White;
                    Console.ReadKey();
                }
                    Console.Clear();
            }
        }

    }
}
