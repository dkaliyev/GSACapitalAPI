using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

namespace Utilities.Calculators
{
    public class CumulativePnLCalculator: ICalculator<List<ProfitNLoss>, List<CumulativePNL>>
    {
        public List<CumulativePNL> Calculate(List<ProfitNLoss> input)
        {
            List<CumulativePNL> result = new List<CumulativePNL>();

            var grouped = input.GroupBy(x => new { x.Strategy.Region, x.Date }).ToList();

            foreach (var pair in grouped)
            {
                var cumulativePNL = new CumulativePNL();
                cumulativePNL.region = pair.Key.Region;
                cumulativePNL.cumulativePnl = pair.Sum(x => x.Value);
                cumulativePNL.date = pair.Key.Date.ToString("yyyy-MM-dd");
                result.Add(cumulativePNL);
            }

            return result;
        }
    }
}
