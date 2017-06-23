using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace Utilities.Mappers
{
    public class ProfitNLossMapper
    {
        public List<ProfitNLossDTO> ToDTO(string header, List<string> lines)
        {
            var result = new List<ProfitNLossDTO>();
            var headerCols = header.Split(',');
            var headerCount = headerCols.Length;

            foreach (var line in lines)
            {

                var cols = line.Split(',');

                if (cols.Length != headerCount)
                {
                    continue;
                }

                

                for (int i = 1; i < headerCount; i++)
                {
                    var capital = new ProfitNLossDTO();

                    capital.Date = DateTime.Parse(cols[0]);
                    capital.Value = decimal.Parse(cols[i]);
                    capital.Strategy = headerCols[i];
                    result.Add(capital);
                }

                
            }

            return result;
        }
    }
}
