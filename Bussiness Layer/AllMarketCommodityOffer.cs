using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using DataAccessLayer;
using log4net;





namespace BussinessLayer

{

    public class AllMarketCommodityOffer

    {
        private List<ComInfo> lci;

        static ILog myLogger = LogManager.GetLogger("mylogger");
        public AllMarketCommodityOffer()

        {

        }

        public List<ComInfo> SendQueryAllMarketRequest()

        {

            SimpleHTTPClient client = new SimpleHTTPClient();

            QueryAllMarket request = new QueryAllMarket();

            request.type = "queryAllMarket";

            Token x = new Token();
            string nonce = "" + x.getNonce();
            string token = x.createToken();

            this.lci = client.SendPostRequest<QueryAllMarket, List<ComInfo>>(x.getUrl(), x.getUserName(), nonce, token, request);
            myLogger.Info("the user send request about all comoddities offers");

            return this.lci;




        }

        public void printList()

        {



            foreach (ComInfo n in this.lci)

            {



                Console.WriteLine("Id:" + " " + n.Id + " " + "Ask price:" + " " + n.info.ask + " " + "Bid price:" + " " + n.info.bid);
                Console.WriteLine();
            }

        }
        public string ToStringA()

        {

            string output = "";

            foreach (ComInfo n in this.lci)
            {



                output = output + Environment.NewLine + "Id:" + " " + n.Id + " " + "Ask price:" + " " + n.info.ask + " " + "Bid price:" + " " + n.info.bid;

            }
            return output;

        }

    }








}

