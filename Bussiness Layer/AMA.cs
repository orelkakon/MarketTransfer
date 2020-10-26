using System;
using System.Collections.Generic;
using System.Windows;
using System.Timers;
using log4net;
using System.IO;
using DataAccessLayer;
using System.Data.SqlClient;

namespace BussinessLayer
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class AMA
    {
        static ILog myLogger = LogManager.GetLogger("mylogger");
        
        private static System.Timers.Timer aTimer;
        private SqlData sql;
        public AMA()
        {
            sql = new SqlData();
            sql.connect();
            SetTimer();
            

        }
        public void run()
        {
            aTimer.Start();
        }

        public void stop()
        {
            aTimer.Stop();

        }

        private void Button_Click(object senders, RoutedEventArgs e)
        {
           
            run();
   
            

        }

        public void SetTimer()
        {
            // Create a timer with a ten second interval.
            aTimer = new System.Timers.Timer(20000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += onTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = false;
        }
        private void onTimedEvent(Object source, ElapsedEventArgs e)
        { 
            myLogger.Info("AMA start to work");
             sql.close();
            sql.connect();
            MarketUserData MYUSER = new MarketUserData();
            MarketUserData myUser = MYUSER.SendQueryUserRequest();
            AllMarketCommodityOffer MACO = new AllMarketCommodityOffer();
            List<ComInfo> cominfo = MACO.SendQueryAllMarketRequest();
            SqlDataReader rdr = sql.sendCommand("SELECT top 100 commodity, AVG(price) as 'average' FROM (SELECT commodity, price, timestamp FROM items where buyer = 20) commodity group by commodity order by commodity");
            int counterS = 0;
            string Scom = "";

            while (counterS < 6 && rdr.Read())
            {
                Scom = "" + rdr["commodity"];
                if (Convert.ToInt32(Scom) > 3 && myUser.commodities[Scom] > 1)
                {
                    SellRequest SR = new SellRequest();
                    int price = Math.Max(Convert.ToInt32(rdr["average"]) + Convert.ToInt32(rdr["average"]) * 15 / 100, cominfo[Convert.ToInt32(rdr["commodity"])].info.bid);
                    int amount = Convert.ToInt32(myUser.commodities[Scom] / 10) + 1;
                    int commodity = Convert.ToInt32(rdr["commodity"]);
                    int response = SR.SendSellRequest(price, commodity, amount);
                    if (response != -1)
                    {
                        DateTime date = DateTime.Now;
                        string sdate = date.ToString();                       
                        string path = "c:\\History.txt";
                        using (StreamWriter wr = File.AppendText(path))
                        {
                            wr.WriteLine("Request");
                            wr.WriteLine("sell");
                            wr.WriteLine(response);
                            wr.WriteLine(Convert.ToInt32(myUser.commodities[Scom] / 10) + 1);
                            wr.WriteLine(price);
                            wr.WriteLine(Convert.ToInt32(rdr["commodity"]));
                            wr.WriteLine("true");
                            wr.WriteLine(sdate.ToString());
                        }
                    }
                    counterS++;
                }

            }
            rdr.Close();

            SqlDataReader rdr2 = sql.sendCommand("SELECT top 100 commodity, AVG(price) as 'average' FROM (SELECT commodity, price, timestamp FROM items where seller = 20) commodity group by commodity order by commodity");
            int counterB = 0;
            while (counterB < 6 && rdr2.Read())
            {
                Scom = "" + rdr2["commodity"];
                if (Convert.ToInt32(Scom) > 3 && myUser.funds > 5000)
                {
                    BuyRequest SB = new BuyRequest();
                    int price = Math.Min(Convert.ToInt32(rdr2["average"]) - Convert.ToInt32(rdr2["average"]) * 15 / 100, cominfo[Convert.ToInt32(rdr2["commodity"])].info.ask);
                    int response = SB.sendBuyRequest(price, Convert.ToInt32(rdr2["commodity"]), Convert.ToInt32(myUser.commodities[Scom] / 10) + 1);
                    if (response != -1)
                    {
                        DateTime date = DateTime.Now;
                        string sdate = date.ToString();
                        string path = "c:\\History.txt";
                        using (StreamWriter wr = File.AppendText(path))
                        {
                            wr.WriteLine("Request");
                            wr.WriteLine("buy");
                            wr.WriteLine(response);
                            wr.WriteLine(Convert.ToInt32(myUser.commodities[Scom] / 10) + 1);
                            wr.WriteLine(price);
                            wr.WriteLine(Convert.ToInt32(rdr2["commodity"]));
                            wr.WriteLine("true");
                            wr.WriteLine(sdate.ToString());
                        }
                    }
                }
                counterB++;
            }
            rdr2.Close();
            sql.close();
        }
    }
}