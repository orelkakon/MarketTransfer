using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using BussinessLayer;
using GUI;
using System.Collections.Generic;
using Moq;

namespace UnitTestProject
{

    [TestFixture]
    public class UnitTest1
    {
        private int[] buy;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            buy = new int[4];
            BuyRequest s = new BuyRequest();
            buy[0] = s.sendBuyRequest(3, 5, 3);
            buy[1] = s.sendBuyRequest(5, 2, 3);
            buy[2] = s.sendBuyRequest(2, 3, 3);
            buy[3] = s.sendBuyRequest(1, 4, 3);
            // this code runs once no matter how many tests are in this class
        }

        [Test]
        public void Test1Buy()
        {
            //var mock = new Mock<ISerializer>;
            BuyRequest buy = new BuyRequest();
            int i = buy.sendBuyRequest(4, 2, 10);
            Assert.AreNotEqual(-1, i);
            i = buy.sendBuyRequest(1000000900, 3, 10);
            Assert.AreEqual(-1, i);
        }

        [Test]
        public void Test2Sell()
        {
            SellRequest sell = new SellRequest();
            if (buy[0] != 1)
                Assert.AreNotEqual(-1, sell.SendSellRequest(1000, 5, 3));
            else
                Assert.AreEqual(-1, sell.SendSellRequest(1000, 5, 3));
        }
        [Test]
        public void Test3AMA()
        {
          /*  MainWindow one = new MainWindow();
            one.window2.Button_StopClick(null, null);
            one.ButtonBuy_Click(null, null);*/
        }
        [Test]
        public void Test4QueryBuySellRequest()
        {
            MarketItemQuery s = new MarketItemQuery();
            int i = 0;
            for (int j = 0; j < 4; j++)
            {
                if (buy[j] == -1)
                {
                    i = buy[j];
                }

            }
            Assert.AreEqual(null, s.SendQueryBuySellRequest(0));
        }

        [Test]
        public void Test5History()
        {
        }

        [Test]
        public void Test6()
        {
        }

        [Test]
        public void Test7()
        {

        }

    }
}

