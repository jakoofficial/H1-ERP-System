using System;
using System.Collections.Generic;
using System.Drawing.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace H1_ERP_System
{
    public class Checker
    {
        /// <summary>
        /// Goes through the objects properties and returns false if one or more is null
        /// </summary>
        /// <param name="o">Object to look through</param>
        /// <returns>If object properties is null</returns>
        public static bool ChecksIfEmpty(object o)
        {//https://stackoverflow.com/questions/41275797/check-if-any-property-of-class-is-null
         //Comment by Rob (Dec 22, 2016)
            if (o.GetType().GetProperties().All(p => p.GetValue(o) != null))
            {
                bool isNull = o.GetType().GetProperties().All(p => p.GetValue(o).ToString() != "");
                return !isNull; //Returns opposite value for clarity
            }
            else { return true; }
        }

        public static bool Retry()
        {
            Console.Clear();
            Console.WriteLine("TThere might be an empty value, please make sure everything has a value.\n\n" +
                              "Press ENTER to try again\n" +
                              "Or ESCAPE to quit editing\n");
            ConsoleKey key = Console.ReadKey().Key;
            Console.Clear();
            if (key == ConsoleKey.Enter)
            {
                return true;
            }
            return false;
        }

    }
}
