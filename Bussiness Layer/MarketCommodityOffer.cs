using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;


namespace BussinessLayer
{
    public class MarketCommodityOffer : MarketClient.DataEntries.IMarketCommodityOffer
    {

        public int ask
        {
            get;
            set;
        }
        public int bid
        {
            get;
            set;
        }

        public MarketCommodityOffer()
        {

        }

        public MarketCommodityOffer SendQueryMarketRequest(int commodity)
        {

            SimpleHTTPClient client = new SimpleHTTPClient();
            QueryMarket request = new QueryMarket();
            request.type = "queryMarket";
            request.commodity = commodity;
            Token x = new Token();
            string token = x.createToken();
            string nonce = "" + x.getNonce();
            MarketCommodityOffer response = client.SendPostRequest<QueryMarket, MarketCommodityOffer>(x.getUrl(), x.getUserName(), nonce, token, request);
            string s = response.ToStringA();
            return response;

        }
        public void Print()
        {
            Console.WriteLine("ask price is:" + " " + ask);
            Console.WriteLine("bid price is:" + " " + bid);

        }
        public string ToStringA()
        {
            string output = "ask price is:" + " " + ask + Environment.NewLine + "bid price is:" + " " + bid;
            return output;
        }
    }
}