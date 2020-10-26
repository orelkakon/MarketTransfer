using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;


namespace BussinessLayer
{
    public class MarketUserRequests
    {


        public MarketUserRequests()
        {
        }
        public string SendQueryUserRequests()
        {
            SimpleHTTPClient client = new SimpleHTTPClient();
            QueryUserRequests request = new QueryUserRequests();
            request.type = "queryUserRequests";

            Token x = new Token();
            string token = x.createToken();
            string nonce = "" + x.getNonce();

            string temp = client.SendPostRequest<QueryUserRequests>(x.getUrl(), x.getUserName(), nonce, token, request);
            string response = MarketClient.Utils.SimpleCtyptoLibrary.decrypt(temp, x.getPrivateKey());
            if (response != "[]")
            {
                string[] stringSeparators = new string[] { "}, {" };
                string[] output = response.Split(stringSeparators, StringSplitOptions.None);
                int k = 1;
                string Foutput = "";
                for (int i = 0; i < output.Length - 1; i = i + 1)
                {
                    Foutput = Foutput + output[i] + "}" + "\n" + "{";
                }
                Foutput = Foutput + output[output.Length - 1];
                return Foutput;
            }
            else
                return response;

        }

    }
}
