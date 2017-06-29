using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Entities;
using DTO;

namespace Utilities.Calculators
{
    public class CompoundDailyReturnCalculator: ICalculator<Tuple<List<Capital>, List<ProfitNLoss>, string>, List<CompoundDailyReturnDTO>>
    {
        public List<CompoundDailyReturnDTO> Calculate(Tuple<List<Capital>, List<ProfitNLoss>, string> input)
        {
            var result = new List<CompoundDailyReturnDTO>();
            var cumulative = 0.0;

            var capitals = input.Item1;
            var pnls = input.Item2.GroupBy(x => new { x.Date.Year, x.Date.Month }).ToList();

            foreach (var pair in pnls)
            {
                var capital =
                    capitals.FirstOrDefault(x => x.Date.Year == pair.Key.Year && x.Date.Month == pair.Key.Month).Value;
                if (capital > 0)
                {
                    var value = pair.Sum(x => Math.Round(x.Value / (capital * 1.0m), 5));
                    var cmpdReturn = new CompoundDailyReturnDTO();
                    cmpdReturn.strategy = input.Item3;
                    cmpdReturn.compoundReturn = value;
                    cmpdReturn.date = new DateTime(pair.Key.Year, pair.Key.Month, 1).ToString("yyyy-MM-dd");
                    result.Add(cmpdReturn);
                }
            }

            return result;
        }
    }
}
