using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_KKT_Online
{
    public static class Extensions
    {
        public static decimal ToDecimal(this int price)
        {
            string pr = price.ToString();

            if (pr.Length >= 3)
                return decimal.Parse(pr.Substring(0, pr.Length - 2) + "." + pr.Substring(pr.Length - 3, 2).ToString());
            else
                return Convert.ToDecimal(price);
        }
    }
}
