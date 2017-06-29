using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Mappers
{
    public class CapitalMapper
    {
        public List<CapitalDTO> ToDTO(string header, List<string> lines)
        {
            var result = new List<CapitalDTO>();
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
                    var capital = new CapitalDTO();

                    capital.Date = DateTime.Parse(cols[0]);
                    capital.Value = long.Parse(cols[i]);
                    capital.Name = headerCols[i];
                    result.Add(capital);
                }

            }

            return result;
        }
    }
}
