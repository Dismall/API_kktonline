using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_KKT_Online
{
    public class Check
    {
        public Check()
        {
            this.Items = new HashSet<CheckItem>();
        }
        public decimal CashTotalSum { get; set; }
        public decimal ECashTotalSum { get; set; }
        public decimal TotalSum { get; set; }
        public decimal Nds10 { get; set; }
        public decimal Nds18 { get; set; }
        public DateTime dateTime { get; set; }
        public Shop ShopInfo { get; set; }
        public CashRegister CashBox { get; set; }
        public string json { get; set; }
        public ICollection<CheckItem> Items { get; set; }
    }
}
