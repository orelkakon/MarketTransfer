using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BussinessLayer;
using System.Text.RegularExpressions;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Tester : Window
    {
        private List<List<KeyValuePair<string, double>>> AskBidGraph
        {
            get;
            set;
        }

        public Tester()
        {
            InitializeComponent();
            ShowColumnChart();
        }

        private void ShowColumnChart()
        {
            SqlData sql = new SqlData();
            sql.connect();
            //Graph of avarage of each commodity
            double[] valueAv = sql.averangeOnly();
            List<KeyValuePair<string, double>> Avg = new List<KeyValuePair<string, double>>();
            Avg.Add(new KeyValuePair<string, double>("C-0", valueAv[0]));
            Avg.Add(new KeyValuePair<string, double>("C-1", valueAv[1]));
            Avg.Add(new KeyValuePair<string, double>("C-2", valueAv[2]));
            Avg.Add(new KeyValuePair<string, double>("C-3", valueAv[3]));
            Avg.Add(new KeyValuePair<string, double>("C-4", valueAv[4]));
            Avg.Add(new KeyValuePair<string, double>("C-5", valueAv[5]));
            Avg.Add(new KeyValuePair<string, double>("C-6", valueAv[6]));
            Avg.Add(new KeyValuePair<string, double>("C-7", valueAv[7]));
            Avg.Add(new KeyValuePair<string, double>("C-8", valueAv[8]));
            Avg.Add(new KeyValuePair<string, double>("C-9", valueAv[9]));

            //Graph of Hot commodties 
            double[] valueHo = sql.hotCommodities();
            List<KeyValuePair<string, double>> HotCo = new List<KeyValuePair<string, double>>();
            HotCo.Add(new KeyValuePair<string, double>("C-0", valueHo[0]));
            HotCo.Add(new KeyValuePair<string, double>("C-1", valueHo[1]));
            HotCo.Add(new KeyValuePair<string, double>("C-2", valueHo[2]));
            HotCo.Add(new KeyValuePair<string, double>("C-3", valueHo[3]));
            HotCo.Add(new KeyValuePair<string, double>("C-4", valueHo[4]));
            HotCo.Add(new KeyValuePair<string, double>("C-5", valueHo[5]));
            HotCo.Add(new KeyValuePair<string, double>("C-6", valueHo[6]));
            HotCo.Add(new KeyValuePair<string, double>("C-7", valueHo[7]));
            HotCo.Add(new KeyValuePair<string, double>("C-8", valueHo[8]));
            HotCo.Add(new KeyValuePair<string, double>("C-9", valueHo[9]));

            //Graph of each commodity with her ask price and bid price
            AllMarketCommodityOffer MACO = new AllMarketCommodityOffer();
            List<ComInfo> AskBid = MACO.SendQueryAllMarketRequest();
            List<KeyValuePair<string, double>> bidColumn = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<string, double>> askColumn = new List<KeyValuePair<string, double>>();
            for (int i = 0; i < 10; i++)
            {
                bidColumn.Add(new KeyValuePair<string, double>("C-" + AskBid[i].Id + " ", AskBid[i].info.bid));
                askColumn.Add(new KeyValuePair<string, double>("C-" + AskBid[i].Id + " ", AskBid[i].info.ask));
            }
            this.AskBidGraph = new List<List<KeyValuePair<string, double>>>();
            AskBidGraph.Add(bidColumn);
            AskBidGraph.Add(askColumn);

            //Graph of each commodity with her amount
            MarketUserData MUD = new MarketUserData();
            MarketUserData ComAmount = MUD.SendQueryUserRequest();
            List<KeyValuePair<string, double>> COfUser = new List<KeyValuePair<string, double>>();
            COfUser.Add(new KeyValuePair<string, double>("C-0", ComAmount.commodities["0"]));
            COfUser.Add(new KeyValuePair<string, double>("C-1", ComAmount.commodities["1"]));
            COfUser.Add(new KeyValuePair<string, double>("C-2", ComAmount.commodities["2"]));
            COfUser.Add(new KeyValuePair<string, double>("C-3", ComAmount.commodities["3"]));
            COfUser.Add(new KeyValuePair<string, double>("C-4", ComAmount.commodities["4"]));
            COfUser.Add(new KeyValuePair<string, double>("C-5", ComAmount.commodities["5"]));
            COfUser.Add(new KeyValuePair<string, double>("C-6", ComAmount.commodities["6"]));
            COfUser.Add(new KeyValuePair<string, double>("C-7", ComAmount.commodities["7"]));
            COfUser.Add(new KeyValuePair<string, double>("C-8", ComAmount.commodities["8"]));
            COfUser.Add(new KeyValuePair<string, double>("C-9", ComAmount.commodities["9"]));

            //Setting data for column chart
            columnChartG.DataContext = AskBidGraph;

            //Setting data for pie chart
            columnChartA.DataContext = Avg;

            //Setting data for pie chart
            Hot.DataContext = HotCo;

            //Setting data for column chart
            Cou.DataContext = COfUser;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ButtonRefersh(object sender, RoutedEventArgs e)
        {
            Tester t = new Tester();
            t.Show();
            this.Close();
        }

        private void ButtonSearch(object sender, RoutedEventArgs e)
        {
            string input = Se.Text;
            string output = "";
            SqlData Sql = new SqlData();
            Sql.connect();
            string command = @"select commodity,max(price) as 'max',min(price) as 'min' from
(
select top" + " " + input + " " + "commodity,price from items order by timestamp ) a group by commodity";
            SqlDataReader myReader = Sql.sendCommand(command);
            output = "Com :" + "   " + "Max :" + "    " + "Min :" + "\n";
            while (myReader.Read())
            {
                output = output + Convert.ToInt32(myReader["commodity"]) + "       " + myReader["max"] + "          " + myReader["min"] + "\n";
            }

            MessageBox.Show(output);

            return;
        }
    }
}
