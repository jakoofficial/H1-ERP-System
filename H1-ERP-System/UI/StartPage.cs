using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System.UI
{
    public class StartPage
    {
        /// <summary>
        /// StartUp() - 
        /// </summary>
        public static void StartUp()
        {
        repeat:

            Console.Clear();
            Console.WriteLine($"=== ERP ===");
            Console.WriteLine("1. Companies in Database. \n2. Check specific company. \n3. Close the program.");
            Console.Write("> ");

            int.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case 1:
                    Screen.Display(new CompanyScreen());
                    break;
                case 2:
                    Screen.Display(new CompanyInfoScreen());
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Try Again.");
                    Console.ReadKey();
                    goto repeat;
            }
        }
    }
}
