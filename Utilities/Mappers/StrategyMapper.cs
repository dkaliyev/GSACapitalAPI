using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using DTO;

namespace Utilities.Mappers
{
    public class StrategyMapper
    {
        public List<StrategyDTO> ToDTO(List<string> lines)
        {
            var result = new List<StrategyDTO>();
            
            foreach (var line in lines)
            {
                var pair = line.Split(',');

                var dto = new StrategyDTO();

                dto.Name = pair[0];
                dto.Region = pair[1];

                result.Add(dto);
            }

            return result;
        }
    }
}
