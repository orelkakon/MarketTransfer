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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using BussinessLayer;
using System.Collections.ObjectModel;
using log4net;
using System.IO;
using System.Xml.Serialization;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<HistoryItem> History { get; set; } = new ObservableCollection<HistoryItem>();
        static ILog myLogger = LogManager.GetLogger("mylogger");

        public MainWindow()
        {

            myLogger.Info("GUI was started");


            InitializeComponent();
            DataContext = this;
            Window2 window2 = new Window2(this);
            window2.Show();
            using (StreamReader rd = new StreamReader("..\\history.txt"))
            {
                String text = rd.ReadToEnd();
                //MessageBox.Show(text);
                try
                {
                    // MessageBox.Show(text.Substring(0, text.IndexOf("\n")));
                    while (text != null)
                    {
                        if (text.Substring(0, text.IndexOf("\n")).Trim().Equals("Request"))
                        {
                            text = text.Substring(text.IndexOf("\n") + 1);

                            string type = text.Substring(0, text.IndexOf("\n"));
                            text = text.Substring(text.IndexOf("\n") + 1).Trim();
                            int id = Convert.ToInt32(text.Substring(0, text.IndexOf("\n")));
                            text = text.Substring(text.IndexOf("\n") + 1).Trim();
                            int amount = Convert.ToInt32(text.Substring(0, text.IndexOf("\n")));
                            text = text.Substring(text.IndexOf("\n") + 1).Trim();
                            int price = Convert.ToInt32(text.Substring(0, text.IndexOf("\n")));
                            text = text.Substring(text.IndexOf("\n") + 1).Trim();
                            int commodity = Convert.ToInt32(text.Substring(0, text.IndexOf("\n")));
                            text = text.Substring(text.IndexOf("\n") + 1).Trim();
                            bool isAMA = false;
                            if (text.Substring(0, text.IndexOf("\n") + 1).Trim().Equals("true"))
                                isAMA = true;
                            text = text.Substring(text.IndexOf("\n") + 1).Trim();
                            string date;
                            if (text.IndexOf("\n") == -1)
                            {
                                date = text;
                                text = null;
                            }
                            else
                            {
                                date = text.Substring(0, text.IndexOf("\n"));
                                text = text.Substring(text.IndexOf("\n") + 1).Trim();
                            }
                            HistoryItem h = new HistoryItem(type, id, amount, price, commodity, isAMA, date);
                            History.Insert(0, h);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

            }


        }





        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ButtonSell_Click(object sender, RoutedEventArgs e)
        {
            myLogger.Info("Sell button was pressed");
            string c = commodity.Text;
            string a = amount.Text;
            string p = price.Text;
            if (c.Length != 0 & a.Length != 0 & p.Length != 0)
            {
                int cI = Convert.ToInt32(c);
                int aI = Convert.ToInt32(a);
                int pI = Convert.ToInt32(p);
                SellRequest SR = new SellRequest();
                int output = SR.SendSellRequest(pI, cI, aI);
                if (output == -1)
                {
                    MessageBox.Show("Failed");
                }
                else
                {
                    MessageBox.Show("The sell is done:request id:" + output);
                    EnterHistory("Sell", cI, aI, pI, output, false);
                }
            }
            else
                MessageBox.Show("full all the fields");
            commodity.Text = "";
            amount.Text = "";
            price.Text = "";
        }
        private void ButtonBuy_Click(object sender, RoutedEventArgs e)
        {
            myLogger.Info("Buy button was pressed");
            string c = commodity.Text;
            string a = amount.Text;
            string p = price.Text;
            if (c.Length != 0 & a.Length != 0 & p.Length != 0)
            {
                int cI = Convert.ToInt32(c);
                int aI = Convert.ToInt32(a);
                int pI = Convert.ToInt32(p);
                BuyRequest SB = new BuyRequest();
                int output = SB.sendBuyRequest(pI, cI, aI);
                if (output == -1)
                {
                    MessageBox.Show("Failed:");
                }
                else
                {
                    MessageBox.Show("The Buy is Done:request id:" + output);
                    EnterHistory("Buy", cI, aI, pI, output, false);
                }

            }
            else
                MessageBox.Show("full all the fields");

            commodity.Text = "";
            amount.Text = "";
            price.Text = "";
        }
        private void ButtonAllCancel_Click(object sender, RoutedEventArgs e)
        {
            myLogger.Info("Cancel All Request's button was pressed");

            try
            {

                MarketUserData MUD = new MarketUserData();
                MarketUserData AllReq = MUD.SendQueryUserRequest();
                if (AllReq.requests.Count != 0)
                {
                    foreach (int id in AllReq.requests)
                    {
                        System.Threading.Thread.Sleep(300); // delay the function on 0.8 sec to prevent ban from the server
                        int IdSent = Convert.ToInt32(id);
                        MarketItemQuery MIQ = new MarketItemQuery();
                        MarketItemQuery output2 = MIQ.SendQueryBuySellRequest(IdSent);
                        CancelBuySellRequest CBSR = new CancelBuySellRequest();
                        bool output = CBSR.SendCancelBuySellRequest(IdSent);
                        if (output)
                            EnterHistory("Cancel", output2.commodity, output2.amount, output2.price, IdSent, false);
                    }
                }
                MessageBox.Show("All requests was delete");
            }
            catch (Exception e7)
            {
                Console.WriteLine(e7.ToString());
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            myLogger.Info("Cancel Request button was pressed");
            string input = requestId.Text;
            if (input.Length != 0)
            {
                int id = Convert.ToInt32(input);
                if (id < 0)
                {
                    MessageBox.Show("Bad request id");
                    requestId.Text = "";
                    return;
                }
                try
                {
                    MarketItemQuery MIQ = new MarketItemQuery();
                    MarketItemQuery output2 = MIQ.SendQueryBuySellRequest(id);
                    CancelBuySellRequest CBSR = new CancelBuySellRequest();
                    bool output = CBSR.SendCancelBuySellRequest(id);
                    if (output)
                    {
                        MessageBox.Show("DONE");
                        EnterHistory("Cancel", output2.commodity, output2.amount, output2.price, id, false);

                    }
                    else
                    {
                        MessageBox.Show("Error  NOT DONE");

                    }
                }
                catch (Exception e3)
                {
                    MessageBox.Show("Error , NOT DONE");
                    myLogger.Error("the cancel of the request" + id + " failed because:id not found");
                }

            }
            else
                MessageBox.Show("full the field");
            requestId.Text = "";
        }

        private void ButtonBSrequest_Click(object sender, RoutedEventArgs e)
        {

            myLogger.Info("the Sell/Buy query button is pressed");
            string input = requestId.Text;
            if (input.Length != 0)
            {
                int id = Convert.ToInt32(input);
                MarketItemQuery MIQ = new MarketItemQuery();


                try
                {
                    MarketItemQuery output = MIQ.SendQueryBuySellRequest(id);
                    MessageBox.Show(output.ToStringA());
                    myLogger.Info("the request to know about the user's request:" + id + " is done successfuly");

                }
                /* catch (MarketException e)
                 {

                 }*/
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);
                    myLogger.Error("the request to know about the user's request:" + id + " is failed because the id of the request not found");

                }
            }
            else MessageBox.Show("Full the field");
            requestId.Text = "";



        }
        private void ButtonCoffer_Click(object sender, RoutedEventArgs e)
        {
            myLogger.Info("Commodity Offer button was pressed");
            string input = commodityid.Text;

            if (input.Length != 0)
            {
                try
                {
                    int id = Convert.ToInt32(input);
                    MarketCommodityOffer MCO = new MarketCommodityOffer();
                    MarketCommodityOffer output = MCO.SendQueryMarketRequest(id);
                    MessageBox.Show(output.ToStringA());
                    myLogger.Info("the request about commodity :" + id + " done successfuly");
                }
                catch (Exception e2)
                {
                    MessageBox.Show("Bad commodity");
                    int id = Convert.ToInt32(input);
                    myLogger.Error("the request about commodity :" + id + " failed because of BAD COMMODITY");
                }
            }
            else MessageBox.Show("full the field");
            commodityid.Text = "";
        }
        private void ButtonAllCoffer_Click(object sender, RoutedEventArgs e)
        {
            myLogger.Info("All Commodities Offers button was pressed");
            AllMarketCommodityOffer MACO = new AllMarketCommodityOffer();
            List<ComInfo> output = MACO.SendQueryAllMarketRequest();
            MessageBox.Show(MACO.ToStringA());
            myLogger.Info("the request to know about all commodities offers is done");


        }
        private void ButtonMState_Click(object sender, RoutedEventArgs e)
        {
            myLogger.Info("Market State button was pressed");
            MarketUserData MUD = new MarketUserData();
            MarketUserData output = MUD.SendQueryUserRequest();
            MessageBox.Show(output.ToStringA());
            myLogger.Info("the request to know about user's market state is done");
        }
        private void ButtonMyReq_Click(object sender, RoutedEventArgs e)
        {
            myLogger.Info("User Requests button was pressed");
            MarketUserRequests QUR = new MarketUserRequests();
            string output = QUR.SendQueryUserRequests();
            MessageBox.Show(output);
            myLogger.Info("the request to know about all user's requests is done");
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            myLogger.Info("The application was close");
            MessageBox.Show("Bye Bye");
            Application.Current.Shutdown();
        }

        private void EnterHistory(string type, int commodity, int amount, int price, int id, bool isAMA)
        {
            DateTime dateTime = DateTime.Now;
            string date = dateTime.ToString();
            HistoryItem h = new HistoryItem(type, id, amount, price, commodity, isAMA, date);
            this.History.Insert(0, h);
            string path = "..\\History.txt";
            using (StreamWriter wr = File.AppendText(path))
            {
                wr.WriteLine("Request");
                wr.WriteLine(type);
                wr.WriteLine(id);
                wr.WriteLine(amount);
                wr.WriteLine(price);
                wr.WriteLine(commodity);
                if (isAMA)
                    wr.WriteLine("true");
                else
                    wr.WriteLine("false");
                wr.WriteLine(date.ToString());


            }

            /* string path = "C:\\Users\\Nati\\Desktop\\16517\\GUI\\historyItems.xml";
             XmlSerializer xs = new XmlSerializer(typeof(HistoryItem));
             using (StreamWriter wr = File.AppendText(path))
             {
                 xs.Serialize(wr,h);
             }*/
            myLogger.Info(type + " request:" + id + " " + "added to history");
        }


        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Table_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CancelAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCreateReport_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();
            report.createReport();
        }

        private void Graph_Click(object sender, RoutedEventArgs e)
        {
            Tester t = new Tester();
            t.Show();



        }
    }

}