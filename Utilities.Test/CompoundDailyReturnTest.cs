using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.Calculators;

namespace Utilities.Test
{
    [TestClass]
    public class CompoundDailyReturnTest
    {
        [TestMethod]
        public void CalculateTest()
        {
            var pnls = new List<ProfitNLoss>
            {
                new ProfitNLoss
                {
                    Date = DateTime.Parse("2012-01-02"),
                    Value = 6324,
                    Strategy = new Strategy
                    {
                        Name = "Strategy1"
                    }
                },
                new ProfitNLoss
                {
                    Date = DateTime.Parse("2012-01-03"),
                    Value = 1200,
                    Strategy = new Strategy
                    {
                        Name = "Strategy1"
                    }
                },
                new ProfitNLoss
                {
                    Date = DateTime.Parse("2012-01-04"),
                    Value = 5300,
                    Strategy = new Strategy
                    {
                        Name = "Strategy1"
                    }
                },
                new ProfitNLoss
                {
                    Date = DateTime.Parse("2012-01-05"),
                    Value = 2453,
                    Strategy = new Strategy
                    {
                        Name = "Strategy1"
                    }
                }
            };

            var capitals = new List<Capital>
            {
                new Capital
                {
                    Date = DateTime.Parse("2012-01-01"),
                    Value = 1000000,
                    Strategy = new Strategy
                    {
                        Name = "Strategy1",
                        Region = "EU"
                    }
                }
            };

            var calculator = new CompoundDailyReturnCalculator();

            var result =
                calculator.Calculate(new Tuple<List<Capital>, List<ProfitNLoss>, string>(capitals, pnls, "Strategy1"));

            Assert.AreEqual(0.01527m, result.First().compoundReturn);
        }
    }
}
