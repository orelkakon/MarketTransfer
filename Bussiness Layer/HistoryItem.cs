using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace BussinessLayer
{
    public class HistoryItem
    {
        public string type { get; set; }
        public int id { get; set; }
        public int amount { get; set; }
        public int price { get; set; }
        public int commodity { get; set; }
        public bool isAMA { get; set; }
        public string date { get; set; }
        private HistoryItem()
        {

        }
        public HistoryItem(string type, int id, int amount, int price, int commodity, bool isAMA, string date)
        {
            this.type = type;
            this.id = id;
            this.amount = amount;
            this.commodity = commodity;
            this.price = price;
            this.isAMA = isAMA;
            this.date = date;
        }
    }
}
