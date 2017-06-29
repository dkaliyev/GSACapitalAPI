using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CumulativePNL
    {
        public string region { get; set; }
        public string date { get; set; }
        public long cumulativePnl { get; set; }
    }
}
