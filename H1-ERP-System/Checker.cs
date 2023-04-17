using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
