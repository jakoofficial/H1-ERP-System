using H1_ERP_System.CustomerFolder;
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
        /// Goes through the objects properties and returns true if one or more is null
        /// </summary>
        /// <param name="o">Object to look through</param>
        /// <returns>If object properties is null</returns>
        public static bool IfEmpty(object o)
        {//https://stackoverflow.com/questions/41275797/check-if-any-property-of-class-is-null
         //Comment by Rob (Dec 22, 2016)
            if (o.GetType().GetProperties().All(p => p.GetValue(o) != null))
            {
                bool isNull = o.GetType().GetProperties().All(p => p.GetValue(o).ToString() != "");
                return !isNull; //Returns opposite value for clarity
            }
            else { return true; }
        }

        public static void MenuOptionColorSetter(ConsoleColor foreColor, ConsoleColor backColor, string text = "")
        {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.WriteLine(text);

        }

        /// <summary>
        /// Yes or no question that return true if yes or false if no
        /// </summary>
        /// <param name="deleteItem">Name of the item for deletion. Leave empty for "data"</param>
        /// <returns>true or false</returns>
        public static bool DeleteData(string deleteItem = "data")
        {
            Console.Clear();
            int option = 1;
            ConsoleKeyInfo key;
            bool selected = false;
            (int left, int top) = Console.GetCursorPosition();
            int colorChecker = 1;


            Console.Clear();
            while (!selected)
            {
                Console.SetCursorPosition(left, top);
                MenuOptionColorSetter(ConsoleColor.White, ConsoleColor.Black, $"\nAre you sure you wanna delete this {deleteItem}?");

                if (colorChecker == 1)
                {
                    MenuOptionColorSetter(ConsoleColor.Black, ConsoleColor.White, "Yes");
                    MenuOptionColorSetter(ConsoleColor.White, ConsoleColor.Black, "No");
                }
                if (colorChecker == 2)
                {
                    MenuOptionColorSetter(ConsoleColor.White, ConsoleColor.Black, "Yes");
                    MenuOptionColorSetter(ConsoleColor.Black, ConsoleColor.White, "No");
                }

                ConsoleKeyInfo Key = Console.ReadKey(true);
                Console.CursorVisible = false;

                switch (Key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 2 ? 1 : option + 1);
                        colorChecker = (colorChecker == 2 ? 1 : colorChecker + 1);
                        break;

                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 2 : option - 1);
                        colorChecker = (colorChecker == 1 ? 2 : colorChecker - 1);
                        break;

                    case ConsoleKey.Enter:
                        selected = true;

                        if (option == 1) return true;
                        else return false;
                        break;
                    default:
                        return false;
                }
            }
            return false;
            //if (option == 1)
            //    Database.DeleteCustomer(cos);
            //Console.Clear();
        }


        public static bool Retry()
        {
            Console.Clear();
            Console.WriteLine("TThere might be an empty value, please make sure everything has a value.\n\n" +
                              "Press ENTER to try again\n" +
                              "Or ESCAPE to quit editing\n");
            ConsoleKey key = Console.ReadKey().Key;
            //Console.Clear();
            if (key == ConsoleKey.Enter)
            {
                return true;
            }
            return false;
        }

    }
}
