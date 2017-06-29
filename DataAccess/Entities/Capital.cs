using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Capital
    {
        public Guid ID { get; set; }
        public long Value { get; set; }
        public Strategy Strategy { get; set; }
        public DateTime Date { get; set; }
    }
}
