using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CompoundDailyReturnDTO
    {
        public string strategy { get; set; }
        public string date { get; set; }
        public decimal compoundReturn { get; set; }
    }
}
