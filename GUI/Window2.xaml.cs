using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BussinessLayer;
using System.Timers;
using log4net;
using System.IO;
using DataAccessLayer;
using System.Data.SqlClient;
using System.Reflection;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        static ILog myLogger = LogManager.GetLogger("mylogger");
        public MainWindow mainWin;
        private static System.Timers.Timer aTimer;
        private SqlData sql;
        public Window2(MainWindow win)
        {
            myLogger.Info("windows2 was opened");
            mainWin = win;
            mainWin.Hide();
            win.IsEnabled = false;
            sql = new SqlData();
            sql.connect();
            SetTimer();
            InitializeComponent();

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
            mainWin.IsEnabled = false;
            run();
            MessageBox.Show("the AMA is working");
            myLogger.Info("AMA start to work");
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int Va = 0;
            try
            {
                mainWin.Show();
                mainWin.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Va++;
                string message = "You closed the program" + Environment.NewLine + "So you need to reload" + Environment.NewLine + "You can click quit ";
                MessageBox.Show(message);
            }
            if (Va == 0)
            {
                MessageBox.Show("the AMA is stopped");
                myLogger.Info("AMA stop to work");
            }
            stop();
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
            sql.close();
            sql.connect();
            MarketUserData MYUSER = new MarketUserData();
            MarketUserData myUser = MYUSER.SendQueryUserRequest();
            AllMarketCommodityOffer MACO = new AllMarketCommodityOffer();
            List<ComInfo> cominfo = MACO.SendQueryAllMarketRequest();
            SqlDataReader rdr = sql.sendCommand("SELECT top 100 commodity, AVG(price) as 'average' FROM (SELECT commodity, price, timestamp FROM items where buyer = 20) commodity group by commodity order by commodity");
            int counterS = 0;
            string Scom = "";
            while (counterS < 10 && rdr.Read())
            {
                Scom = "" + rdr["commodity"];
                if (Convert.ToInt32(Scom) > 3 && myUser.commodities[Scom] > 1)
                {
                    SellRequest SR = new SellRequest();
                    int price = Math.Max(Convert.ToInt32(rdr["average"]) + Convert.ToInt32(rdr["average"]) * 20 / 100, cominfo[Convert.ToInt32(rdr["commodity"])].info.bid);
                    int amount = Convert.ToInt32(myUser.commodities[Scom] / 20) + 1;
                    int commodity = Convert.ToInt32(rdr["commodity"]);
                    int response = SR.SendSellRequest(price, commodity, amount);
                    if (response != -1)
                    {
                        DateTime date = DateTime.Now;
                        string sdate = date.ToString();
                        HistoryItem h = new HistoryItem("Sell", response, Convert.ToInt32(myUser.commodities[Scom] / 20) + 1, price, Convert.ToInt32(rdr["commodity"]), true, sdate);
                        // MessageBox.Show("SellDone");//for test
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            mainWin.History.Insert(0, h);
                        });
                        string path = "..\\History.txt";
                        using (StreamWriter wr = File.AppendText(path))
                        {
                            wr.WriteLine("Request");
                            wr.WriteLine("sell");
                            wr.WriteLine(response);
                            wr.WriteLine(Convert.ToInt32(myUser.commodities[Scom] / 20) + 1);
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
            while (counterB < 10 && rdr2.Read())
            {
                Scom = "" + rdr2["commodity"];
                if (Convert.ToInt32(Scom) > 3 && myUser.funds > 5000)
                {
                    BuyRequest SB = new BuyRequest();
                    int price = Math.Min(Convert.ToInt32(rdr2["average"]) - Convert.ToInt32(rdr2["average"]) * 20 / 100, cominfo[Convert.ToInt32(rdr2["commodity"])].info.ask);
                    int response = SB.sendBuyRequest(price, Convert.ToInt32(rdr2["commodity"]), Convert.ToInt32(myUser.commodities[Scom] / 20) + 1);
                    if (response != -1)
                    {
                        DateTime date = DateTime.Now;
                        string sdate = date.ToString();
                        HistoryItem h = new HistoryItem("Buy", response, Convert.ToInt32(myUser.commodities[Scom] / 20) + 1, price, Convert.ToInt32(rdr2["commodity"]), true, sdate);
                        //MessageBox.Show("BuyDone");//for test
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            mainWin.History.Insert(0, h);
                        });
                        string path = "..\\History.txt";
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