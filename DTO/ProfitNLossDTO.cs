using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ProfitNLossDTO
    {
        public decimal Value { get; set; }
        public string Strategy { get; set; }
        public DateTime Date { get; set; }
    }
}
