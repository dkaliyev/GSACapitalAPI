using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Entities;
using Utilities.Calculators;

namespace Utilities.Test
{
    [TestClass]
    public class CumulativePnLCalculatorTest
    {
        [TestMethod]
        public void CalculateTest()
        {
            var pnls = new List<ProfitNLoss>
            {
                new ProfitNLoss()
                {
                    Date = DateTime.Parse("2012-01-01"),
                    Strategy = new Strategy()
                    {
                        Name = "Strategy1",
                        Region = "EU"
                    },
                    Value = 2000
                },
                new ProfitNLoss()
                {
                    Date = DateTime.Parse("2012-01-01"),
                    Strategy = new Strategy()
                    {
                        Name = "Strategy2",
                        Region = "EU"
                    },
                    Value = 1000
                },
                new ProfitNLoss()
                {
                    Date = DateTime.Parse("2012-02-01"),
                    Strategy = new Strategy()
                    {
                        Name = "Strategy3",
                        Region = "US"
                    },
                    Value = 1200
                },
                new ProfitNLoss()
                {
                    Date = DateTime.Parse("2012-02-01"),
                    Strategy = new Strategy()
                    {
                        Name = "Strategy4",
                        Region = "US"
                    },
                    Value = 3000
                }
            };

            var calculator = new CumulativePnLCalculator();

            var result = calculator.Calculate(pnls);

            Assert.AreEqual(2, result.Count);

            var eu = result.First(x => x.date == "2012-01-01" && x.region == "EU");
            var us = result.First(x => x.date == "2012-02-01" && x.region == "US");

            Assert.AreEqual(3000, eu.cumulativePnl);
            Assert.AreEqual(4200, us.cumulativePnl);
        }
    }
}
