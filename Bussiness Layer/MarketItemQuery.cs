using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BussinessLayer
{
    public class MarketItemQuery : MarketClient.DataEntries.IMarketItemQuery
    {
        public object auth;
        public string type;
        public int commodity;
        public int amount;
        public int price;

        public MarketItemQuery()
        {

        }

        public MarketItemQuery SendQueryBuySellRequest(int id)
        {
            SimpleHTTPClient client = new SimpleHTTPClient();
            QueryBuySell request = new QueryBuySell();
            request.type = "queryBuySell";
            request.id = id;
            Token x = new Token();
            string token = x.createToken();
            string nonce = "" + x.getNonce();
            MarketItemQuery response = client.SendPostRequest<QueryBuySell, MarketItemQuery>(x.getUrl(), x.getUserName(), nonce, token, request);
            return response;


        }
        public void Print()
        {
            Console.WriteLine("commodity:" + commodity);
            Console.WriteLine("amount:" + amount);
            Console.WriteLine("price:" + price);
        }
        public string ToStringA()
        {
            string output = "commodity:" + commodity + Environment.NewLine + "amount:" + amount + Environment.NewLine + "price:" + price;
            return output;
        }

    }
}
