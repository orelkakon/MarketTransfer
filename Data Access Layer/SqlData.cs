using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace DataAccessLayer
{
    public class SqlData
    {

        private SqlConnection myConnection;
        public SqlData()
        {
            connect();
        }

        public void connect()
        {

            string connectionString = @"Data Source=ise172.ise.bgu.ac.il;Initial Catalog=history;User ID=labuser;Password=wonsawheightfly";
            myConnection = new SqlConnection(connectionString);
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //calculate avarage of each commodity and return the values in array
        public double[] averangeOnly()
        {
            double[] value = new double[10];
            string command = @"
            SELECT
               commodity,AVG(price) as 'average'
            FROM
            ( 
             SELECT top 10000
               commodity, price, timestamp
             FROM
               items
               order by timestamp
            )
               commodity group by commodity order by commodity";  //in this line was here 's' instead of first commodity
            SqlCommand CS = new SqlCommand(command, myConnection);
            SqlDataReader Sql = CS.ExecuteReader();
            while (Sql.Read())
            {
                double i = Convert.ToDouble(Sql["average"]);
                value[(Convert.ToInt32(Sql["commodity"]))] = i;
            }
            Sql.Close();
            return value;
        }

        //calculate the number of deals with each commodity and return the values in array
        public double[] hotCommodities()
        {
            int AmountOfDeals = 0;
            int ComNum = 0;
            string command = @"SELECT
    commodity,count(*) as 'count'
FROM
items
group by commodity order by commodity";
            SqlCommand CS = new SqlCommand(command, myConnection);
            SqlDataReader Sql = CS.ExecuteReader();

            double[] value = new double[10];
            while (Sql.Read())
            {
                AmountOfDeals = (Convert.ToInt32(Sql["count"]));
                value[ComNum] = AmountOfDeals;
                ComNum++;
            }
            Sql.Close();
            return value;
        }




        public SqlDataReader sendCommand(string command)
        {
            SqlCommand myCommand = new SqlCommand(command, myConnection);
            return myCommand.ExecuteReader();
        }

        public void close()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }


}