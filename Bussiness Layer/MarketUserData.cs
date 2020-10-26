using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;


namespace BussinessLayer
{
    public class MarketUserData : MarketClient.DataEntries.IMarketUserData
    {

        public Dictionary<string, int> commodities;
        public float funds;
        public List<int> requests;

        public MarketUserData()
        {
            requests = null;
        }

        public MarketUserData SendQueryUserRequest()
        {
            SimpleHTTPClient client = new SimpleHTTPClient();

            QueryUser request = new QueryUser();
            request.type = "queryUser";
            Token x = new Token();
            string token = x.createToken();
            string nonce = "" + x.getNonce();
            MarketUserData response = client.SendPostRequest<QueryUser, MarketUserData>(x.getUrl(), x.getUserName(), nonce, token, request);

            return response;
        }

        public void Print()
        {
            Console.WriteLine("commodities:");
            foreach (var comm in commodities)
            {
                Console.WriteLine(comm);
            }

            Console.WriteLine("funds:" + funds);
            Console.WriteLine("requests:");
            for (int i = 0; i < requests.Count; i++)
            {
                Console.WriteLine(requests[i]);
            }


        }
        public string ToStringA()
        {

            string output = "commodities:";
            foreach (var comm in commodities)
            {
                output = output + Environment.NewLine + comm;
            }

            output = output + Environment.NewLine + "funds:" + funds + Environment.NewLine + "requests:";

            for (int i = 0; i < requests.Count; i++)
            {
                output = output + Environment.NewLine + requests[i];
            }
            return output;
        }

    }
}
