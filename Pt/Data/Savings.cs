using System;
using System.Collections.Generic;

namespace Pt.Data
{
    public partial class Savings
    {
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public long Id { get; set; }
    }
}
