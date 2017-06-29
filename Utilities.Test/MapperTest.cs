using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.Mappers;

namespace Utilities.Test
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void TestToDTO()
        {
            var pnls = new List<string> {"2010-01-01,95045,501273,429834,-352913"};
            var pnlsHeader = "Date,Strategy1,Strategy2,Strategy3,Strategy4";

            var pnlMapper = new ProfitNLossMapper();

            var pnlDTOs = pnlMapper.ToDTO(pnlsHeader, pnls);

            foreach (var profitNLossDto in pnlDTOs)
            {
                Assert.AreEqual("2010-01-01", profitNLossDto.Date.Date.ToString("yyyy-MM-dd"));
            }
            

            Assert.AreEqual(95045, pnlDTOs.First(x=>x.Strategy == "Strategy1") .Value);
            Assert.AreEqual(501273, pnlDTOs.First(x=>x.Strategy == "Strategy2") .Value);
            Assert.AreEqual(429834, pnlDTOs.First(x=>x.Strategy == "Strategy3") .Value);
            Assert.AreEqual(-352913, pnlDTOs.First(x=>x.Strategy == "Strategy4") .Value);
        }
    }
}
