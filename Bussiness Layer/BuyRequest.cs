using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using log4net;

namespace BussinessLayer
{
    public class BuyRequest
    {
        static ILog myLogger = LogManager.GetLogger("mylogger");
        public string type;
        public int commodity;
        public int amount;
        public int price;

        public BuyRequest()
        {
        }
        public int sendBuyRequest(int price, int commodity, int amount)
        {

            SimpleHTTPClient client = new SimpleHTTPClient();

            this.commodity = commodity;
            this.amount = amount;
            this.price = price;
            this.type = "buy";
            Token x = new Token();
            string token = x.createToken();
            string nonce = "" + x.getNonce();
            string temp = client.SendPostRequest<BuyRequest>(x.getUrl(), x.getUserName(), nonce, token, this);
            string response = MarketClient.Utils.SimpleCtyptoLibrary.decrypt(temp, x.getPrivateKey());

            int output;
            if (response[0] < '1' | response[0] > '9')
            {
                Console.WriteLine(response);
                output = -1;
                myLogger.Error("buy request is failed");
            }
            else
            {
                output = Int32.Parse(response);
                myLogger.Error("buy request is done:" + " " + output);
            }
            return output;
        }
    }
}


