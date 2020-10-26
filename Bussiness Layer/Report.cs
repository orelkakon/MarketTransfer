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

//Added for using the basic iSharp's abilities
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.SqlClient;
using DataAccessLayer;

namespace BussinessLayer
{
    public class Report
    {
        public Report()
        {

        }

        public void createReport()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Report"; // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document
                string res = dlg.FileName;

                var doc1 = new Document();
                PdfWriter.GetInstance(doc1, new FileStream(res, FileMode.Create));

                doc1.Open();
                doc1.Add(new iTextSharp.text.Paragraph("Trading Report" + " " + "user 20:" + "\n"));
                MarketUserData MUD = new MarketUserData();
                MarketUserData output = MUD.SendQueryUserRequest();
                string MS = output.ToStringA();
                doc1.Add(new iTextSharp.text.Paragraph(MS));

                doc1.Add(new iTextSharp.text.Paragraph("\n" + "\n" + "\n"));
                MarketUserRequests QUR = new MarketUserRequests();
                string MRequests = QUR.SendQueryUserRequests();
                if (MRequests != "[]")
                {
                    doc1.Add(new iTextSharp.text.Paragraph("Requests' details:" + "\n" + MRequests + "\n"));
                }
                else
                    doc1.Add(new iTextSharp.text.Paragraph("No requests"));

                SqlData b = new SqlData();
                b.connect();
                SqlDataReader myReader = b.sendCommand(@"SELECT
commodity, count(*) as 'count'
FROM
items
where buyer = 20 
group by commodity order by commodity");

                int[,] value = new int[2, 10];
                int i = 0;
                while (myReader.Read())
                {
                    value[0, i] = Convert.ToInt32(myReader["count"]);
                    i++;
                }
                i = 0;
                b.close();
                b.connect();

                SqlDataReader readsell = b.sendCommand(@"SELECT
commodity, count(*) as 'count'
FROM
items
where seller = 20 
group by commodity order by commodity");
                while (readsell.Read())
                {
                    value[1, i] = Convert.ToInt32(readsell["count"]);
                    i++;
                }
                doc1.Add(new iTextSharp.text.Paragraph("\n" + "\n"));
                doc1.Add(new iTextSharp.text.Paragraph("My History Trades(since the Data Base created):  " + "\n"));

                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        doc1.Add(new iTextSharp.text.Paragraph("sum of buy deals with each commodity: "));
                    }
                    else
                    {
                        doc1.Add(new iTextSharp.text.Paragraph("sum of sell deals with each commodity: "));
                    }
                    for (int m = 0; m < 10; m++)
                    {
                        doc1.Add(new iTextSharp.text.Paragraph("Commodity" + " " + m + ": " + value[j, m]));
                    }
                    doc1.Add(new iTextSharp.text.Paragraph(" " + " "));
                }
                b.close();
                doc1.Close();
            }
        }
    }
}
