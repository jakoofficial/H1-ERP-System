using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
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

            Console.WriteLine($"=== ERP ===");
            Console.Clear();
            Console.WriteLine("1. Companies in Database. \n2. Check specific company.");
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
                default:
                    Console.WriteLine("Try Again.");
                    Console.ReadKey();
                    goto repeat;
            }
        }
    }
}
