using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using log4net;


namespace BussinessLayer
{
    public class CancelBuySellRequest
    {
        static ILog myLogger = LogManager.GetLogger("mylogger");
        public string type;
        public int id;

        public CancelBuySellRequest()
        {
        }

        public bool SendCancelBuySellRequest(int id)
        {

            SimpleHTTPClient client = new SimpleHTTPClient();
            this.id = id;
            type = "cancelBuySell";
            Token x = new Token();
            string token = x.createToken();
            string nonce = "" + x.getNonce();
            string temp = client.SendPostRequest<CancelBuySellRequest>(x.getUrl(), x.getUserName(), nonce, token, this);
            string response = MarketClient.Utils.SimpleCtyptoLibrary.decrypt(temp, x.getPrivateKey());

            if (response == "Ok")
            {
                myLogger.Info("the cancel of the request" + id + " done");
                return true;
            }
            return false;




        }
    }
}
